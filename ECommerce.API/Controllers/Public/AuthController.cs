using ECommerce.API.Models.Auth;
using ECommerceApp.DataAccessLayer.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginRequest = ECommerce.API.Models.Auth.LoginRequest;

namespace ECommerce.API.Controllers.Public
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest requeset)
        {
            if(string.IsNullOrWhiteSpace(requeset.Email) || string.IsNullOrWhiteSpace(requeset.Password))
            {
                return BadRequest(new { message = "Email and password are required" });
            }

            var user = await _userManager.FindByEmailAsync(requeset.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var signIn=await _signInManager.CheckPasswordSignInAsync(user,requeset.Password,lockoutOnFailure:false);
            if (!signIn.Succeeded)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            string tokenString =await GenerateJwtToken(user);

            var expiresMinutes = int.TryParse(_config["Jwt:ExpireMinutes"], out var m) ? m : 60;

            return Ok(new LoginResponse
            {
                AccessToken = tokenString,
                ExpiresIn = expiresMinutes * 60
            });

        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> Register([FromBody] Models.Auth.RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FullName) 
                || string.IsNullOrWhiteSpace(request.Password) 
                || string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest(new { message = "FullName, Password, Email are required" });
            }

            if (!string.Equals(request.Password, request.ConfirmPassword, StringComparison.Ordinal))
            {
                return BadRequest(new { message = "Password and Confirm Password do not match" });
            }

            var existingMail = await _userManager.FindByEmailAsync(request.Email);
            if (existingMail != null) 
            {
                return BadRequest(new { message = "Email is already exist" });
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName
            };

            var isCreated= await _userManager.CreateAsync(user,request.Password);
            if (!isCreated.Succeeded)
            {
                return BadRequest(new { message = "Registration failed !" });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
            if (!roleResult.Succeeded)
            {
                return BadRequest(new { message = "Could not assign customer role." });
            }

            var claimResult = await _userManager.AddClaimAsync(user, new Claim("FullName",user.FullName));
            if (!claimResult.Succeeded)
            {
                return BadRequest(new { message = "Could not add FullName claim." });
            }

            string tokenString = await GenerateJwtToken(user);

            var expiresMinutes = int.TryParse(_config["Jwt:ExpireMinutes"], out var m) ? m : 60;

            return Ok(new LoginResponse
            {
                AccessToken = tokenString,
                ExpiresIn = expiresMinutes * 60
            });

        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var jwtIssuer = _config["Jwt:Issuer"];
            var jwtAudience = _config["Jwt:Audience"];
            var jwtKey = _config["Jwt:Key"];
            var expiresMinutes = int.TryParse(_config["Jwt:ExpireMinutes"], out var m) ? m : 60;

            var roles=await _userManager.GetRolesAsync(user);
            var userClaims= await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Email, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            foreach (var c in userClaims)
            {
                if (c.Type == "FullName")
                {
                    claims.Add(new Claim("FullName", c.Value));
                }
            }

            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(expiresMinutes);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
                );

            var tokenString=new JwtSecurityTokenHandler().WriteToken(token);   
            return tokenString;
        }
    }
}

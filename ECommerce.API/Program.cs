using ECommerce.API.Middlewares;
using ECommerceApp.BusinessLayer.Modules.Carts;
using ECommerceApp.BusinessLayer.Modules.Carts.Interfaces;
using ECommerceApp.BusinessLayer.Modules.Categories;
using ECommerceApp.BusinessLayer.Modules.Categories.Interface;
using ECommerceApp.BusinessLayer.Modules.Orders;
using ECommerceApp.BusinessLayer.Modules.Orders.Interfaces;
using ECommerceApp.BusinessLayer.Modules.Products;
using ECommerceApp.BusinessLayer.Modules.Products.Interface;
using ECommerceApp.DataAccessLayer.Data;
using ECommerceApp.DataAccessLayer.Identity;
using ECommerceApp.DataAccessLayer.Modules.Carts;
using ECommerceApp.DataAccessLayer.Modules.Carts.Inerfaces;
using ECommerceApp.DataAccessLayer.Modules.Categories;
using ECommerceApp.DataAccessLayer.Modules.Categories.Interfaces;
using ECommerceApp.DataAccessLayer.Modules.Orders;
using ECommerceApp.DataAccessLayer.Modules.Orders.Interfaces;
using ECommerceApp.DataAccessLayer.Modules.Products;
using ECommerceApp.DataAccessLayer.Modules.Products.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Carts;
using ECommerceApp.PresentationLayer.Modules.Carts.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Categories;
using ECommerceApp.PresentationLayer.Modules.Categories.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders;
using ECommerceApp.PresentationLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Products;
using ECommerceApp.PresentationLayer.Modules.Products.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var jwtKey = builder.Configuration["Jwt:Key"];


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();
builder.Services.AddDbContext<ECommerceDbContext>(options =>
options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddAutoMapper(cfg => { }, typeof(CategoryMappingProfile).Assembly);
//Session related services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ECommerceDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,

        ValidateAudience = true,
        ValidAudience = jwtAudience,

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,

        ValidateIssuerSigningKey =true,
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
    };

});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddScoped<IdentityRoleSeeder>();

builder.Services.AddScoped<ICategoryViewModelProvider, CategoryViewModelProvider>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IProductViewModelProvider, ProductViewModelProvider>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ICartRepository, SessionCartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartViewModelProvider, CartViewModelProvider>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICheckoutViewModelProvider, CheckoutViewModelProvider>();

var app = builder.Build();

#region Minimal API dummy data
//var products = new List<Product>
//{
//    new Product { Id = 1, Name = "Laptop", Price = 1000 },
//    new Product { Id = 2, Name = "Phone", Price = 500 },
//    new Product { Id = 3, Name = "Book", Price = 1500 },

//};
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

#region Minimal API

////Get All products
//app.MapGet("/products", () =>
//{
//    return products;
//});

////Get By Id
//app.MapGet("/products/{id}", (int id) =>
//{
//    var product = products.FirstOrDefault(p => p.Id == id);
//    return product;
//});

////Post
//app.MapPost("/products", (Product product) =>
//{
//    product.Id = products.Max(p => p.Id) + 1;
//    products.Add(product);
//    return product;

//});

////PUT
//app.MapPut("/products/{id}", (int id,Product updatedProduct) =>
//{
//    var product = products.FirstOrDefault(p => p.Id == id);
//    if (product is null) return null;

//    product.Name = updatedProduct.Name;
//    product.Price = updatedProduct.Price;
//    return product;
//});

////PUT
//app.MapPatch("/products/{id}", (int id, Product updatedProduct) =>
//{
//    var product = products.FirstOrDefault(p => p.Id == id);
//    if (product is null) return null;

//    if (!string.IsNullOrEmpty(updatedProduct.Name))
//    {
//        product.Name = updatedProduct.Name;
//    }

//    if (updatedProduct.Price > 0)
//    {
//        product.Price = updatedProduct.Price;
//    }

//    return product;
//});

//app.MapDelete("/products/{id}", (int id) =>
//{
//    var product = products.FirstOrDefault(p => p.Id == id);
//    if (product is null) return null;

//    products.Remove(product);

//    return products;
//});

#endregion

app.UseExceptionHandler(options => { });

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seed = scope.ServiceProvider.GetRequiredService<IdentityRoleSeeder>();
    await seed.SeedAsync();
}

app.Run();


//public class Product
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public decimal Price { get; set; }
//}

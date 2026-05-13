using ECommerceApp.BusinessLayer.Exceptions;
using ECommerceApp.PresentationLayer.Modules.Categories.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Categories.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class CategoriesController:ControllerBase
    {
        private readonly ICategoryViewModelProvider _categoryViewModelProvider;
        public CategoriesController(ICategoryViewModelProvider categoryViewModelProvider)
        {
            _categoryViewModelProvider = categoryViewModelProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryListViewModel>>> GetAll()
        {
            var categories = await _categoryViewModelProvider.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryEditViewModel>> GetById(int id)
        {
            var vm= await _categoryViewModelProvider.GetByIdAsync(id);
            if(vm == null)
            {
                throw new Exception("Id not found");
                //return NotFound();
            }
            return Ok(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateViewModel model)
        {

            try
            {
                var created = await _categoryViewModelProvider.AddAsync(model);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidUserInputException ex)
            {
                ModelState.AddModelError(nameof(CategoryCreateViewModel.Name), ex.Message);
                return ValidationProblem(ModelState);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryEditViewModel model)
        {
            if (id != model.Id) 
                return BadRequest(new { message = "Route id and body id do not match." });

            try
            {
                await _categoryViewModelProvider.UpdateAsync(model);
                return NoContent();
            }
            catch (InvalidUserInputException ex)
            {
                ModelState.AddModelError(nameof(CategoryEditViewModel.Name), ex.Message);
                return ValidationProblem(ModelState);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _categoryViewModelProvider.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}

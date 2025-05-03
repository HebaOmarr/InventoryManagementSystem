using InventoryManagementSystem.DAL.UnitOfWork;
using InventoryManagementSystem.Entities.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.BLL.DTOs.Category;


namespace InventoryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDTO categorydto)
        {
            if (categorydto == null)
            {
                return BadRequest("Category cannot be null");
            }
            Category category = new Category
            {
                Name = categorydto.Name,
            };
            await unitOfWork.Category.AddAsync(category);
           await unitOfWork.Save();
            return Ok();
        }
    }
}

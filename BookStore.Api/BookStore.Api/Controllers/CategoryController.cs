using BookStore.Models.ViewModels;
using BookStore.Models.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        readonly CategoryRepository _categoryRepository = new();

        [HttpGet("list")]
        [ProducesResponseType(typeof(ListResponse<CategoryModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetCategories(int pageIndex = 1, int pageSize = 10, string keyword = "")
        {
           var categories = _categoryRepository.GetCategories(pageIndex, pageSize, keyword);
            ListResponse<CategoryModel> listResponse = new()
            {
                records = categories.records.Select(c => new CategoryModel(c)).ToList(),
                totalRecords = categories.totalRecords
            };

            return Ok(listResponse);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound(new { message = "Category not found!" });
            }
            CategoryModel response = new(category);
            return Ok(response);
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
        public IActionResult AddCategory(CategoryModel model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Please fill out the required infomation!" });
            }
            Category category = new()
            {
                Id = model.Id,
                Name = model.Name,
            };
            var response = _categoryRepository.AddCategory(category);
            CategoryModel categoryModel = new(response);
            return Ok(categoryModel);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(CategoryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            Category category = new()
            {
                Id = model.Id,
                Name = model.Name,
            };
            var response = _categoryRepository.UpdateCategory(category);
            if (response == null)
            {
                return NotFound(new { message = "Category not found!" });
            }
            CategoryModel categoryModel = new(response);
            return Ok(categoryModel);
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(NotFoundObjectResult), (int)HttpStatusCode.NotFound)]
        public IActionResult DeleteCategory(int id)
        {
            var response = _categoryRepository.DeleteCategory(id);
            if (response == false)
            {
                return NotFound(new { message = "Category not found!" });
            }
            return Ok(response);
        }
    }
}

using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.DTOs.CategoryDTOs;
using Fiorella.Domain.Entities;
using Fiorella.Persistence.Exceptions;
using Fiorella.Persistence.Inplementations.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fiorella.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CatagoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CatagoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("id")]
        public async Task<IActionResult> get(int id)
        {

            var category = await _categoryService.getById(id);
            return Ok(category);

        }
        [HttpPost]
        public async Task<IActionResult> Post(CategoryCreateDto categoryCreateDto)
        {
                await _categoryService.CreateAsync(categoryCreateDto);
                return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
                List<CategoryGetDto> List = await _categoryService.GetAllAsync();
                return Ok(List);
        }
    }
}

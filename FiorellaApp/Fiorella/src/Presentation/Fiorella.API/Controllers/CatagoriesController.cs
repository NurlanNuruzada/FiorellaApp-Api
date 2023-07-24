using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.DTOs;
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
        public async Task<IActionResult>get(int id)
        {
            try
            {
                var category = await _categoryService.getById(id);
                return Ok(category);
            }
            catch (DublicatedException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(CategoryCreateDto categoryCreateDto)
        {
            try
            { 
                await _categoryService.CreateAsync(categoryCreateDto);
                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (DublicatedException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError , ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            try
            {
                List<CategoryGetDto> List = await _categoryService.GetAllAsync();
                return Ok(List);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

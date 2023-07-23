using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.DTOs;
using Fiorella.Persistence.Exceptions;
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
			try
			{
				CategoryGetDto result = await _categoryService.GetByIdAsync(id);
				return Ok(result);
			}
			catch (NotFoundException ex)
			{

				return StatusCode(ex.StatusCode,ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}
		[HttpPost]
		public async Task<IActionResult> Post(CategoryGetDto categoryCreateDto)
		{
			try
			{
				await _categoryService.CreateAsync(categoryCreateDto);
				return StatusCode((int)HttpStatusCode.Created);
			}
			catch (DublicatedException ex)
			{

				return StatusCode(ex.StatusCode,ex.CustomMessage);
			}
			catch(Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError,ex.Message);
			}

		}
		[HttpGet]

		public async Task<IActionResult> GetAll()
		{

			try
			{
				List<CategoryGetDto> result= await _categoryService.GetAllAsync();
				return Ok(result);
			}
			 
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}


		}
	}
}

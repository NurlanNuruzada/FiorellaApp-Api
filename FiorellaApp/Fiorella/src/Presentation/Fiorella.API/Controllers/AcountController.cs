using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiorella.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AcountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> Login(SingInDto loginDto)
        {
           var Response = await _authService.Login(loginDto);
            return Ok(Response);    
        }
    }
}

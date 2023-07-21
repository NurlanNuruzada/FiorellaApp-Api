using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiorella.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoriesController : ControllerBase
    {
        [HttpGet]
        public string get()
        {
            return "HelloWorld";
        }
    }
}

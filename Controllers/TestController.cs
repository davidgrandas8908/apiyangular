using Microsoft.AspNetCore.Mvc;

namespace RegistroEstudiantesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Funciona");
    }
} 
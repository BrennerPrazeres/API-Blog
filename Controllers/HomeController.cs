using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]//Verificar se API está rodando.
        public IActionResult Get()
        {
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ApiNexos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreetingsController : ControllerBase
    {
        const string Greetings = "Thanks for the oportunity";
        // GET: GreetingsController
        [HttpGet]
        public string Index()
        {
            return Greetings;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetStudent()
        {
            string[] studentNames = new string[] { "Monu", "sonu", "siraf", "sabat" };
            return Ok(studentNames);
        }
    }
}

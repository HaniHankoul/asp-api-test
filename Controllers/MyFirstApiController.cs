using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyFirstApiController : ControllerBase
    {
        [HttpGet("MyName",Name ="MyName")]
        public string GetMyName()
        {
            return "Hani :)";
        }
        [HttpGet("YourName",Name ="YourName")]
        public string GetYourName()
        {
            return "Hello world :)";
        }
        [HttpGet("sum/{a}/{b}")]
        public int SumTwo(int a,int b) {
        return a+b;
        }
        [HttpGet("multiply/{a}/{b}")]
        public int MulTwo(int a, int b)
        {
            return a * b;
        }
    }
}

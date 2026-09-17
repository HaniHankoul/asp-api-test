using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsAPIController : ControllerBase
    {
        [HttpGet("GetAllStudents", Name = "GetAllStudents")]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            return Ok(DummyStudent.StudentsList);
        }
    }
}

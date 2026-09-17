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
        [HttpGet("GetStudentById/{id}", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> GetStudentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid student ID.");
            }
            var student = DummyStudent.StudentsList.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound("Student with id " + id + " not found.");
            }
            return Ok(student);
        }
        [HttpGet("GetPassedStudents", Name = "GetPassedStudents")]
        public ActionResult<List<Student>> GetPassedStudents()
        {
            var passedStudents = DummyStudent.StudentsList.Where(s => s.Grade >= 50).ToList();
            return Ok(passedStudents);
        }

        //post
        [HttpPost("AddStudent", Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student> AddStudent(Student newStudent)
        {
            if (newStudent == null || string.IsNullOrWhiteSpace(newStudent.Name) || newStudent.Age <= 0 || newStudent.Grade < 0)
            {
                return BadRequest("Invalid student data.");
            }
            newStudent.Id = DummyStudent.StudentsList.Count > 0 ? DummyStudent.StudentsList.Max(s => s.Id) + 1 : 1;
            DummyStudent.StudentsList.Add(newStudent);
            return CreatedAtRoute("GetStudentById", new { id = newStudent.Id }, newStudent);
        }

        //delete
        [HttpDelete("DeleteStudent/{id}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid student ID.");
            }
            var student = DummyStudent.StudentsList.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound("Student with id " + id + " not found.");
            }
            DummyStudent.StudentsList.Remove(student);
            return Ok("Student with id " + id + " deleted successfully.");
        }

        //put
        [HttpPut("UpdateStudent/{id}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> UpdateStudent(int id, Student updatedStudent)
        {
            if (id <= 0 || updatedStudent == null || string.IsNullOrWhiteSpace(updatedStudent.Name) || updatedStudent.Age <= 0 || updatedStudent.Grade < 0)
            {
                return BadRequest("Invalid student data.");
            }
            var existingStudent = DummyStudent.StudentsList.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
            {
                return NotFound("Student with id " + id + " not found.");
            }
            existingStudent.Name = updatedStudent.Name;
            existingStudent.Age = updatedStudent.Age;
            existingStudent.Grade = updatedStudent.Grade;
            return Ok(existingStudent);
        }
    }
}

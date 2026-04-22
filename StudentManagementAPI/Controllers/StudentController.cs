using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.Model;

namespace StudentManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StudentController(AppDbContext context)
        {
            _context = context;
        }
       
        [HttpPost]
        public IActionResult Add([FromBody] Student student)
        {
            var userId = int.Parse(User.FindFirst("id").Value);

            student.CreatedBy = userId;
            student.UserId = userId;  

            _context.Students.Add(student);
            _context.SaveChanges();

            return Ok(student);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _context.Students
                .Include(s => s.User)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.Email,
                    s.Phone,
                    CreatedBy = s.User.Username
                })
                .ToList();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var student = _context.Students
                .Include(s => s.User)
                .FirstOrDefault(s => s.Id == id);

            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Student updated)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            student.Name = updated.Name;
            student.Email = updated.Email;
            student.Phone = updated.Phone;
            student.UpdatedAt = DateTime.Now;

            _context.SaveChanges();

            return Ok(student);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();

            return Ok();
        }
    }
}

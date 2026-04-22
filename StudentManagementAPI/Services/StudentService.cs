using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Data;
using StudentManagementAPI.Model;
using StudentManagementAPI.Services;

namespace StudentManagementAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Student student, int userId)
        {
            student.CreatedBy = userId;
            student.UserId = userId;
            student.CreatedAt = DateTime.Now;
            student.UpdatedAt = DateTime.Now;

            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public List<Student> GetAll()
        {
            return _context.Students.Include(s => s.User).ToList();
        }

        public Student GetById(int id)
        {
            return _context.Students.Include(s => s.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(int id, Student updated)
        {
            var student = _context.Students.Find(id);
            if (student == null) return;

            student.Name = updated.Name;
            student.Email = updated.Email;
            student.Phone = updated.Phone;
            student.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return;

            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}

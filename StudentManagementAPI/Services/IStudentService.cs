using StudentManagementAPI.Model;

namespace StudentManagementAPI.Services
{
    public interface IStudentService
    {
        void Add(Student student, int userId);
        List<Student> GetAll();
        Student GetById(int id);
        void Update(int id, Student student);
        void Delete(int id);
    }
}

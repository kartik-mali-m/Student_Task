

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudentManagementAPI.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int CreatedBy { get; set; }

        public int UserId { get; set; }   // ✅ REQUIRED

        [JsonIgnore]
        public User? User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Tiêu đề khóa học không được để trống")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        public string Title { get; set; } = string.Empty;

        [StringLength(4000, ErrorMessage = "Mô tả không được vượt quá 4000 ký tự")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Giảng viên không được để trống")]
        public int InstructorId { get; set; }

        public string InstructorName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public int EnrollmentCount { get; set; }
        public int LessonCount { get; set; }

        // Navigation properties
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
        public List<Assignment> Assignments { get; set; } = new List<Assignment>();
        //public List<Quiz> Quizzes { get; set; } = new List<Quiz>();
    }
}

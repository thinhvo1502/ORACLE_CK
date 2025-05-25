using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Tiêu đề bài học không được để trống")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }

        [Url(ErrorMessage = "URL video không hợp lệ")]
        public string? VideoUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Thứ tự phải lớn hơn 0")]
        public int OrderNumber { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public int Duration { get; set; } // in minutes

        // Navigation properties
        public string CourseName { get; set; } = string.Empty;
    }
}

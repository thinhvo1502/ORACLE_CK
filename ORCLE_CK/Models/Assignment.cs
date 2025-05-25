using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Tiêu đề bài tập không được để trống")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public int MaxScore { get; set; } = 100;

        // Navigation properties
        public string CourseName { get; set; } = string.Empty;
        public int SubmissionCount { get; set; }
    }
}

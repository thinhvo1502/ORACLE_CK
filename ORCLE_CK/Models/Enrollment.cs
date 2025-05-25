using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrolledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;
        public decimal Progress { get; set; } = 0;
        public decimal? FinalGrade { get; set; }

        // Navigation properties
        public string StudentName { get; set; } = string.Empty;
        public string CourseTitle { get; set; } = string.Empty;
        public string InstructorName { get; set; } = string.Empty;
    }

    public enum EnrollmentStatus
    {
        Active = 1,
        Completed = 2,
        Dropped = 3,
        Suspended = 4
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ORCLE_CK.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(100, ErrorMessage = "Tên đăng nhập không được vượt quá 100 ký tự")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vai trò không được để trống")]
        public string Role { get; set; } = "student";

        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; } = true;

        public string RoleDisplayName
        {
            get
            {
                switch (Role?.ToLower())
                {
                    case "admin":
                        return "Quản trị viên";
                    case "instructor":
                        return "Giảng viên";
                    case "student":
                        return "Học viên";
                    default:
                        return "Không xác định";
                }
            }
        }
    }

    public enum UserRole
    {
        Student = 1,
        Instructor = 2,
        Admin = 3
    }
}

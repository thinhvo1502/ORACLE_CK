using ORCLE_CK.Models;
using ORCLE_CK.Utils;
using ORCLE_CK.Data.Repositories;
using ORCLE_CK.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = ORCLE_CK.Exceptions.ValidationException;
namespace ORCLE_CK.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserService() : this(new UserRepository())
        {
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return userRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GetAllUsers: {ex.Message}", ex);
                throw new ServiceException("Không thể tải danh sách người dùng", ex);
            }
        }

        public User GetUserById(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID phải lớn hơn 0");

            try
            {
                return userRepository.GetUserById(userId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GetUserById: {ex.Message}", ex);
                throw new ServiceException("Không thể tải thông tin người dùng", ex);
            }
        }

        public bool CreateUser(User user)
        {
            // Validate user data
            ValidateUser(user);

            try
            {
                // Check if username already exists
                if (userRepository.IsUsernameExists(user.Username))
                {
                    throw new ValidationException("Tên đăng nhập đã tồn tại");
                }

                // Check if email already exists
                if (userRepository.IsEmailExists(user.Email))
                {
                    throw new ValidationException("Email đã tồn tại");
                }

                // Hash password
                user.Password = PasswordHelper.HashPassword(user.Password);

                return userRepository.CreateUser(user);
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in CreateUser: {ex.Message}", ex);
                throw new ServiceException("Không thể tạo người dùng", ex);
            }
        }

        public bool UpdateUser(User user)
        {
            ValidateUser(user, false);

            try
            {
                // Check if username already exists for other users
                var existingUser = userRepository.GetUserByUsername(user.Username);
                if (existingUser != null && existingUser.UserId != user.UserId)
                {
                    throw new ValidationException("Tên đăng nhập đã tồn tại");
                }

                // Check if email already exists for other users
                existingUser = userRepository.GetUserByEmail(user.Email);
                if (existingUser != null && existingUser.UserId != user.UserId)
                {
                    throw new ValidationException("Email đã tồn tại");
                }

                return userRepository.UpdateUser(user);
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in UpdateUser: {ex.Message}", ex);
                throw new ServiceException("Không thể cập nhật người dùng", ex);
            }
        }

        public bool DeleteUser(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID phải lớn hơn 0");

            try
            {
                return userRepository.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in DeleteUser: {ex.Message}", ex);
                throw new ServiceException("Không thể xóa người dùng", ex);
            }
        }

        public User AuthenticateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Tên đăng nhập và mật khẩu không được để trống");

            try
            {
                var user = userRepository.GetUserByUsername(username);

                if (user != null && PasswordHelper.VerifyPassword(password, user.Password))
                {
                    // Update last login time
                    userRepository.UpdateLastLogin(user.UserId);
                    user.LastLoginAt = DateTime.Now;

                    // Clear password for security
                    user.Password = string.Empty;

                    Logger.LogInfo($"User {username} authenticated successfully");
                    return user;
                }

                Logger.LogWarning($"Authentication failed for user {username}");
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in AuthenticateUser: {ex.Message}", ex);
                throw new ServiceException("Lỗi xác thực người dùng", ex);
            }
        }

        public bool ChangePassword(int userId, string currentPassword, string newPassword)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID phải lớn hơn 0");

            if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("Mật khẩu không được để trống");

            if (newPassword.Length < AppSettings.PasswordMinLength)
                throw new ValidationException($"Mật khẩu phải có ít nhất {AppSettings.PasswordMinLength} ký tự");

            try
            {
                var user = userRepository.GetUserById(userId);
                if (user == null)
                    throw new ValidationException("Người dùng không tồn tại");

                // Get user with password for verification
                var userWithPassword = userRepository.GetUserByUsername(user.Username);
                if (userWithPassword == null || !PasswordHelper.VerifyPassword(currentPassword, userWithPassword.Password))
                {
                    throw new ValidationException("Mật khẩu hiện tại không đúng");
                }

                var newPasswordHash = PasswordHelper.HashPassword(newPassword);
                var result = userRepository.ChangePassword(userId, newPasswordHash);

                if (result)
                    Logger.LogInfo($"Password changed for user {userId}");

                return result;
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in ChangePassword: {ex.Message}", ex);
                throw new ServiceException("Không thể đổi mật khẩu", ex);
            }
        }

        public List<User> GetInstructors()
        {
            try
            {
                return userRepository.GetUsersByRole("instructor");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GetInstructors: {ex.Message}", ex);
                throw new ServiceException("Không thể tải danh sách giảng viên", ex);
            }
        }

        public List<User> GetStudents()
        {
            try
            {
                return userRepository.GetUsersByRole("student");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GetStudents: {ex.Message}", ex);
                throw new ServiceException("Không thể tải danh sách học viên", ex);
            }
        }

        private static void ValidateUser(User user, bool validatePassword = true)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user);

            if (!Validator.TryValidateObject(user, validationContext, validationResults, true))
            {
                var errors = string.Join(", ", validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"Dữ liệu không hợp lệ: {errors}");
            }

            if (validatePassword && string.IsNullOrWhiteSpace(user.Password))
                throw new ValidationException("Mật khẩu không được để trống");

            if (validatePassword && user.Password.Length < AppSettings.PasswordMinLength)
                throw new ValidationException($"Mật khẩu phải có ít nhất {AppSettings.PasswordMinLength} ký tự");
        }
    }
}

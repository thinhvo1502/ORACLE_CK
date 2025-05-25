using ORCLE_CK.Data.Repositories;
using ORCLE_CK.Exceptions;
using ORCLE_CK.Models;
using ORCLE_CK.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = ORCLE_CK.Exceptions.ValidationException;
namespace ORCLE_CK.Services
{
    public class CourseService
    {
        private readonly ICourseRepository courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public CourseService() : this(new CourseRepository())
        {
        }

        public List<Course> GetAllCourses()
        {
            try
            {
                return courseRepository.GetAllCourses();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GetAllCourses: {ex.Message}", ex);
                throw new ServiceException("Không thể tải danh sách khóa học", ex);
            }
        }

        public Course GetCourseById(int courseId)
        {
            if (courseId <= 0)
                throw new ArgumentException("Course ID phải lớn hơn 0");

            try
            {
                return courseRepository.GetCourseById(courseId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GetCourseById: {ex.Message}", ex);
                throw new ServiceException("Không thể tải thông tin khóa học", ex);
            }
        }

        public bool CreateCourse(Course course)
        {
            ValidateCourse(course);

            try
            {
                return courseRepository.CreateCourse(course);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in CreateCourse: {ex.Message}", ex);
                throw new ServiceException("Không thể tạo khóa học", ex);
            }
        }

        public bool UpdateCourse(Course course)
        {
            ValidateCourse(course);

            try
            {
                return courseRepository.UpdateCourse(course);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in UpdateCourse: {ex.Message}", ex);
                throw new ServiceException("Không thể cập nhật khóa học", ex);
            }
        }

        public bool DeleteCourse(int courseId)
        {
            if (courseId <= 0)
                throw new ArgumentException("Course ID phải lớn hơn 0");

            try
            {
                return courseRepository.DeleteCourse(courseId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in DeleteCourse: {ex.Message}", ex);
                throw new ServiceException("Không thể xóa khóa học", ex);
            }
        }

        public List<Course> GetCoursesByInstructor(int instructorId)
        {
            try
            {
                return courseRepository.GetCoursesByInstructor(instructorId);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error in GetCoursesByInstructor: {ex.Message}", ex);
                throw new ServiceException("Không thể tải khóa học của giảng viên", ex);
            }
        }

        private static void ValidateCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(course);

            if (!Validator.TryValidateObject(course, validationContext, validationResults, true))
            {
                var errors = string.Join(", ", validationResults.Select(r => r.ErrorMessage));
                throw new ValidationException($"Dữ liệu không hợp lệ: {errors}");
            }
        }
    }
}

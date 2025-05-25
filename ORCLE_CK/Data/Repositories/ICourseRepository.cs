using ORCLE_CK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Data.Repositories
{
    public interface ICourseRepository
    {
        List<Course> GetAllCourses();
        Course GetCourseById(int courseId);
        bool CreateCourse(Course course);
        bool UpdateCourse(Course course);
        bool DeleteCourse(int courseId);
        List<Course> GetCoursesByInstructor(int instructorId);
    }
}

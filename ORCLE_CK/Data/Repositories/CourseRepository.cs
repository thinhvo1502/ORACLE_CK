using Oracle.ManagedDataAccess.Client;
using ORCLE_CK.Models;
using ORCLE_CK.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public List<Course> GetAllCourses()
        {
            var courses = new List<Course>();

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"SELECT c.course_id, c.title, c.description, c.instructor_id, 
                                  c.created_at, c.updated_at, c.is_active,
                                  u.full_name as instructor_name,
                                  (SELECT COUNT(*) FROM Enrollments e WHERE e.course_id = c.course_id) as enrollment_count
                                  FROM Courses c 
                                  LEFT JOIN Users u ON c.instructor_id = u.user_id
                                  ORDER BY c.created_at DESC";

                    using (var command = new OracleCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(MapReaderToCourse(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting all courses: {ex.Message}", ex);
                throw;
            }

            return courses;
        }

        public Course GetCourseById(int courseId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"SELECT c.course_id, c.title, c.description, c.instructor_id, 
                                  c.created_at, c.updated_at, c.is_active,
                                  u.full_name as instructor_name,
                                  (SELECT COUNT(*) FROM Enrollments e WHERE e.course_id = c.course_id) as enrollment_count
                                  FROM Courses c 
                                  LEFT JOIN Users u ON c.instructor_id = u.user_id
                                  WHERE c.course_id = :courseId";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":courseId", OracleDbType.Int32).Value = courseId;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReaderToCourse(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting course by ID {courseId}: {ex.Message}", ex);
                throw;
            }

            return null;
        }

        public bool CreateCourse(Course course)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"INSERT INTO Courses (title, description, instructor_id, is_active) 
                                  VALUES (:title, :description, :instructorId, 1)";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":title", OracleDbType.NVarchar2).Value = course.Title;
                        command.Parameters.Add(":description", OracleDbType.NClob).Value = course.Description ?? (object)DBNull.Value;
                        command.Parameters.Add(":instructorId", OracleDbType.Int32).Value = course.InstructorId;

                        var result = command.ExecuteNonQuery();
                        Logger.LogInfo($"Course {course.Title} created successfully");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error creating course {course.Title}: {ex.Message}", ex);
                throw;
            }
        }

        public bool UpdateCourse(Course course)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"UPDATE Courses SET title = :title, description = :description, 
                                  instructor_id = :instructorId, updated_at = SYSDATE, is_active = :isActive 
                                  WHERE course_id = :courseId";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":title", OracleDbType.NVarchar2).Value = course.Title;
                        command.Parameters.Add(":description", OracleDbType.NClob).Value = course.Description ?? (object)DBNull.Value;
                        command.Parameters.Add(":instructorId", OracleDbType.Int32).Value = course.InstructorId;
                        command.Parameters.Add(":isActive", OracleDbType.Int32).Value = course.IsActive ? 1 : 0;
                        command.Parameters.Add(":courseId", OracleDbType.Int32).Value = course.CourseId;

                        var result = command.ExecuteNonQuery();
                        Logger.LogInfo($"Course {course.Title} updated successfully");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error updating course {course.CourseId}: {ex.Message}", ex);
                throw;
            }
        }

        public bool DeleteCourse(int courseId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    // Soft delete - set is_active to 0
                    string sql = "UPDATE Courses SET is_active = 0 WHERE course_id = :courseId";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":courseId", OracleDbType.Int32).Value = courseId;

                        var result = command.ExecuteNonQuery();
                        Logger.LogInfo($"Course {courseId} deleted successfully");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error deleting course {courseId}: {ex.Message}", ex);
                throw;
            }
        }

        public List<Course> GetCoursesByInstructor(int instructorId)
        {
            var courses = new List<Course>();

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"SELECT c.course_id, c.title, c.description, c.instructor_id, 
                                  c.created_at, c.updated_at, c.is_active,
                                  u.full_name as instructor_name,
                                  (SELECT COUNT(*) FROM Enrollments e WHERE e.course_id = c.course_id) as enrollment_count
                                  FROM Courses c 
                                  LEFT JOIN Users u ON c.instructor_id = u.user_id
                                  WHERE c.instructor_id = :instructorId AND c.is_active = 1
                                  ORDER BY c.created_at DESC";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":instructorId", OracleDbType.Int32).Value = instructorId;

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                courses.Add(MapReaderToCourse(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting courses by instructor {instructorId}: {ex.Message}", ex);
                throw;
            }

            return courses;
        }

        private static Course MapReaderToCourse(IDataReader reader)
        {
            return new Course
            {
                CourseId = Convert.ToInt32(reader["course_id"]),
                Title = reader["title"].ToString() ?? string.Empty,
                Description = reader["description"]?.ToString(),
                InstructorId = Convert.ToInt32(reader["instructor_id"]),
                InstructorName = reader["instructor_name"]?.ToString() ?? string.Empty,
                CreatedAt = Convert.ToDateTime(reader["created_at"]),
                UpdatedAt = reader["updated_at"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["updated_at"]),
                IsActive = Convert.ToInt32(reader["is_active"]) == 1,
                EnrollmentCount = Convert.ToInt32(reader["enrollment_count"])
            };
        }
    }
}

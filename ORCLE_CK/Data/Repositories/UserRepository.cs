using ORCLE_CK.Models;
using ORCLE_CK.Utils;
using ORCLE_CK.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace ORCLE_CK.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"SELECT user_id, full_name, username, email, role, created_at, 
                                  last_login_at, is_active FROM Users ORDER BY created_at DESC";

                    using (var command = new OracleCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(MapReaderToUser(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting all users: {ex.Message}", ex);
                throw;
            }

            return users;
        }

        public User GetUserById(int userId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"SELECT user_id, full_name, username, email, role, created_at, 
                                  last_login_at, is_active FROM Users WHERE user_id = :userId";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReaderToUser(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting user by ID {userId}: {ex.Message}", ex);
                throw;
            }

            return null;
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    username.Trim();
                    string sql = @"SELECT * FROM Users WHERE username = :username AND is_active = 1";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":username", OracleDbType.Varchar2).Value = username;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var user = MapReaderToUser(reader);
                                user.Password = reader["password"].ToString() ?? string.Empty;
                                return user;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting user by username {username}: {ex.Message}", ex);
                throw;
            }

            return null;
        }

        public User GetUserByEmail(string email)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"SELECT user_id, full_name, username, email, role, created_at, 
                                  last_login_at, is_active FROM Users WHERE email = :email";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":email", OracleDbType.Varchar2).Value = email;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReaderToUser(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting user by email {email}: {ex.Message}", ex);
                throw;
            }

            return null;
        }

        public bool CreateUser(User user)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"INSERT INTO Users (full_name, username, email, password, role, is_active) 
                                  VALUES (:fullName, :username, :email, :password, :role, 1)";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":fullName", OracleDbType.NVarchar2).Value = user.FullName;
                        command.Parameters.Add(":username", OracleDbType.Varchar2).Value = user.Username;
                        command.Parameters.Add(":email", OracleDbType.Varchar2).Value = user.Email;
                        command.Parameters.Add(":password", OracleDbType.Varchar2).Value = user.Password;
                        command.Parameters.Add(":role", OracleDbType.Varchar2).Value = user.Role.ToString().ToLower();

                        var result = command.ExecuteNonQuery();
                        Logger.LogInfo($"User {user.Username} created successfully");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error creating user {user.Username}: {ex.Message}", ex);
                throw;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"UPDATE Users SET full_name = :fullName, username = :username, 
                                  email = :email, role = :role, is_active = :isActive 
                                  WHERE user_id = :userId";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":fullName", OracleDbType.NVarchar2).Value = user.FullName;
                        command.Parameters.Add(":username", OracleDbType.Varchar2).Value = user.Username;
                        command.Parameters.Add(":email", OracleDbType.Varchar2).Value = user.Email;
                        command.Parameters.Add(":role", OracleDbType.Varchar2).Value = user.Role.ToString().ToLower();
                        command.Parameters.Add(":isActive", OracleDbType.Int32).Value = user.IsActive ? 1 : 0;
                        command.Parameters.Add(":userId", OracleDbType.Int32).Value = user.UserId;

                        var result = command.ExecuteNonQuery();
                        Logger.LogInfo($"User {user.Username} updated successfully");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error updating user {user.UserId}: {ex.Message}", ex);
                throw;
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    // Soft delete - set is_active to 0
                    string sql = "UPDATE Users SET is_active = 0 WHERE user_id = :userId";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;

                        var result = command.ExecuteNonQuery();
                        Logger.LogInfo($"User {userId} deleted successfully");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error deleting user {userId}: {ex.Message}", ex);
                throw;
            }
        }

        public bool UpdateLastLogin(int userId)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = "UPDATE Users SET last_login_at = SYSDATE WHERE user_id = :userId";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;

                        var result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error updating last login for user {userId}: {ex.Message}", ex);
                throw;
            }
        }

        public bool ChangePassword(int userId, string newPasswordHash)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = "UPDATE Users SET password = :password WHERE user_id = :userId";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":password", OracleDbType.Varchar2).Value = newPasswordHash;
                        command.Parameters.Add(":userId", OracleDbType.Int32).Value = userId;

                        var result = command.ExecuteNonQuery();
                        Logger.LogInfo($"Password changed for user {userId}");
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error changing password for user {userId}: {ex.Message}", ex);
                throw;
            }
        }

        public List<User> GetUsersByRole(string role)
        {
            var users = new List<User>();

            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = @"SELECT user_id, full_name, username, email, role, created_at, 
                          last_login_at, is_active FROM Users 
                          WHERE role = :role AND is_active = 1 ORDER BY full_name";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":role", OracleDbType.Varchar2).Value = role.ToLower();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(MapReaderToUser(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error getting users by role {role}: {ex.Message}", ex);
                throw;
            }

            return users;
        }

        public bool IsUsernameExists(string username)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = "SELECT COUNT(*) FROM Users WHERE username = :username";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":username", OracleDbType.Varchar2).Value = username;

                        var result = command.ExecuteScalar();
                        return Convert.ToInt32(result) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error checking username exists {username}: {ex.Message}", ex);
                throw;
            }
        }

        public bool IsEmailExists(string email)
        {
            try
            {
                using (var connection = DatabaseConnection.GetConnection())
                {
                    connection.Open();

                    string sql = "SELECT COUNT(*) FROM Users WHERE email = :email";

                    using (var command = new OracleCommand(sql, connection))
                    {
                        command.Parameters.Add(":email", OracleDbType.Varchar2).Value = email;

                        var result = command.ExecuteScalar();
                        return Convert.ToInt32(result) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error checking email exists {email}: {ex.Message}", ex);
                throw;
            }
        }

        private static User MapReaderToUser(IDataReader reader)
        {
            return new User
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                FullName = reader["full_name"].ToString() ?? string.Empty,
                Username = reader["username"].ToString() ?? string.Empty,
                Email = reader["email"].ToString() ?? string.Empty,
                Role = reader["role"].ToString() ?? "student",
                CreatedAt = Convert.ToDateTime(reader["created_at"]),
                LastLoginAt = reader["last_login_at"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["last_login_at"]),
                IsActive = Convert.ToInt32(reader["is_active"]) == 1
            };
        }
    }
}

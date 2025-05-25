using ORCLE_CK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ORCLE_CK.Data.Repositories
{
  
        public interface IUserRepository
        {
            List<User> GetAllUsers();
            User GetUserById(int userId);
            User GetUserByUsername(string username);
            User GetUserByEmail(string email);
            bool CreateUser(User user);
            bool UpdateUser(User user);
            bool DeleteUser(int userId);
            bool UpdateLastLogin(int userId);
            bool ChangePassword(int userId, string newPasswordHash);
            List<User> GetUsersByRole(string role);
            bool IsUsernameExists(string username);
            bool IsEmailExists(string email);
        }
    }



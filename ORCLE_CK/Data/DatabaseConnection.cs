using ORCLE_CK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
namespace ORCLE_CK.Data
{
    public static class DatabaseConnection
    {
        private static readonly string connectionString;

        static DatabaseConnection()
        {
            connectionString = ConfigurationManager.ConnectionStrings["OracleConnection"]?.ConnectionString
                ?? throw new InvalidOperationException("Connection string 'OracleConnection' not found in configuration");
        }

        public static OracleConnection GetConnection()
        {
            try
            {
                var connection = new OracleConnection(connectionString);
                return connection;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Database connection error: {ex.Message}", ex);
                throw new Exception($"Không thể kết nối database: {ex.Message}");
            }
        }

        public static bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Database connection test failed: {ex.Message}", ex);
                return false;
            }
        }

        public static void SetConnectionString(string server, string database, string username, string password)
        {
            var newConnectionString = $"Data Source={server}:1521/{database};User Id={username};Password={password};";

            // Update configuration
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings["OracleConnection"].ConnectionString = newConnectionString;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}

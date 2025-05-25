using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ORCLE_CK.Utils
{
    internal class AppSettings
    {
        public static string ApplicationName =>
            ConfigurationManager.AppSettings["ApplicationName"] ?? "E-Learning Management System";

        public static string Version =>
            ConfigurationManager.AppSettings["Version"] ?? "1.0.0";

        public static int MaxLoginAttempts =>
            int.Parse(ConfigurationManager.AppSettings["MaxLoginAttempts"] ?? "3");

        public static int SessionTimeout =>
            int.Parse(ConfigurationManager.AppSettings["SessionTimeout"] ?? "30");

        public static int PasswordMinLength =>
            int.Parse(ConfigurationManager.AppSettings["PasswordMinLength"] ?? "6");

        public static int DatabaseTimeout =>
            int.Parse(ConfigurationManager.AppSettings["DatabaseTimeout"] ?? "30");

        public static string LogLevel =>
            ConfigurationManager.AppSettings["LogLevel"] ?? "Information";

        public static string LogPath =>
            ConfigurationManager.AppSettings["LogPath"] ?? "Logs\\";

        public static bool EnableLogging =>
            bool.Parse(ConfigurationManager.AppSettings["EnableLogging"] ?? "true");
    }
}

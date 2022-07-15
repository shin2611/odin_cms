using Utils.Security;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;

namespace Utils
{
    public sealed class Config
    {
        public static string URL_ROOT { get; set; }
        public static string MEDIA_DISK { get; set; }
        public static string CURRENT_URL_ROOT { get; set; }

        public static string IMAGE_URL { get; set; }
        #region[ConnectionString]
        public static class ConnectionString
        {
            private static string _SQLConn;

            public static string SQLConnWEB { get { return DecryptConnectionString(_SQLConn); } set { _SQLConn = value; } }

            #region AdminConnectionString

            private static string _SQLConnAdmin;
            public static string SQLConnCMS { get { return DecryptConnectionString(_SQLConnAdmin); } set { _SQLConnAdmin = value; } }

            #endregion
        }

        private static string DecryptConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) return string.Empty;
            return new RijndaelEnhanced("odin", "@1B2c3D4e5F6g7H8").Decrypt(connectionString);
        }
        #endregion

        #region[Configuration]
        public static class AppSettings
        {
            private static readonly IConfiguration configuration;
            static AppSettings()
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                configuration = builder.Build();
            }

            public static string Get(string name)
            {
                string appSettings = configuration[name];
                return appSettings;
            }

            #endregion

            #region Utils
            public static bool IsAlpha
            {
                get
                {
                    var isAlpha = Config.AppSettings.Get("IS_ALPHA");
                    if (string.IsNullOrEmpty(isAlpha)) return false;
                    return isAlpha.Trim() == "1" ? true : false;
                }
            }

            public static bool IsBeta
            {
                get
                {
                    var isBeta = Config.AppSettings.Get("IS_BETA");
                    if (string.IsNullOrEmpty(isBeta)) return false;
                    return isBeta.Trim() == "1" ? true : false;
                }
            }

           
            #endregion

        }
    }

}

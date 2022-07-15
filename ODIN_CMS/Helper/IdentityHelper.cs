using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Utils;

namespace ODIN_CMS
{
    public static class AppContext
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public static HttpContext Current => _httpContextAccessor.HttpContext;

        public static string CurrentUserData
        {
            get
            {
                var isAuthen = AppContext.Current.User.Identity.IsAuthenticated;
                return isAuthen ? AppContext.Current.User.FindFirst("UserData").Value : string.Empty;
            }

        }
    }

    public class IdentityHelper
    {
        public static int UserID
        {
            get
            {
                try
                {
                    var userData = AppContext.CurrentUserData;
                    var user = !string.IsNullOrEmpty(userData) ? JsonSerializer.Deserialize<Users>(userData) : new Users();
                    return Convert.ToInt32(user.UserID);
                }
                catch (Exception ex)
                {
                    NLogLogger.PublishException(ex);
                    return 0;
                }
            }



        }

        public static int GroupID
        {
            get
            {
                try
                {
                    var userData = AppContext.CurrentUserData;
                    var user = !string.IsNullOrEmpty(userData) ? JsonSerializer.Deserialize<Users>(userData) : new Users();
                    return Convert.ToInt32(user.GroupID);
                }
                catch (Exception ex)
                {
                    NLogLogger.PublishException(ex);
                    return 0;
                }
            }



        }

        public static int MerchantID
        {
            get
            {
                try
                {
                    var userData = AppContext.CurrentUserData;
                    var user = !string.IsNullOrEmpty(userData) ? JsonSerializer.Deserialize<Users>(userData) : new Users();
                    return Convert.ToInt32(user.MerchantID);
                }
                catch (Exception ex)
                {
                    NLogLogger.PublishException(ex);
                    return 0;
                }
            }



        }

        public static string Username
        {
            get
            {
                try
                {
                    var userData = AppContext.CurrentUserData;
                    var user = !string.IsNullOrEmpty(userData) ? JsonSerializer.Deserialize<Users>(userData) : new Users();
                    return user.Username.ToString();
                }
                catch (Exception ex)
                {
                    NLogLogger.PublishException(ex);
                    return "";
                }
            }
        }

        public static bool IsAdministrator
        {
            get
            {
                try
                {
                    var userData = AppContext.CurrentUserData;
                    var user = !string.IsNullOrEmpty(userData) ? JsonSerializer.Deserialize<Users>(userData) : new Users();
                    return Convert.ToBoolean(user.IsAdministrator);
                }
                catch (Exception ex)
                {
                    NLogLogger.PublishException(ex);
                    return false;
                }
            }
        }
    }

}

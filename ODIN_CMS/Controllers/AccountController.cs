using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ODIN_CMS.Models;
using DataAccess.Implement;
using Utils;
using Utils.Security;
using DataAccess.Factory;
using DataAccess.Models;
using ODIN_CMS.Controllers.Common;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace ODIN_CMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IdentityHelper _identityHelper;
        private IOptions<AppsettingModel> appSettings;

        public AccountController(IConfiguration configuration, IdentityHelper identityHelper)
        {
            _configuration = configuration;
            _identityHelper = identityHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FormLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return Ok(new Response(-1, "Username hoặc Password không được bỏ trống"));
                }
                if (string.IsNullOrEmpty(password))
                {
                    return Ok(new Response(-1, "Username hoặc Password không được bỏ trống"));
                }
                var Password = Encrypt.MD5(password.Trim());
                int checkLogin = AbstractDAOFactory.Instance().CreateUsersDAO().Authentication(username.Trim(), Password);
                NLogLogger.LogInfo("Login-->checkLogin:" + checkLogin);
                if (checkLogin > 0)
                {
                    var m_Users = new UsersDAOImpl().GetByUsername(username);
                    if (m_Users != null && m_Users.UserID > 0)
                    {
                        // lưu thông tinn user 
                        SetSessionValue(m_Users);
                        await SetAuthen(m_Users);
                        return Ok(new Response(1, "Đăng nhập thành công"));
                    }
                }
                return Ok(new Response(-1, "Thông tin đầu vào không đúng"));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task SetAuthen(Users user)
        {
            // create claims
            List<Claim> claims = new List<Claim>
            {
                new Claim("UserData",JsonSerializer.Serialize(user))
            };

            // create identity
            ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

            // create principal
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // sign-in
            await HttpContext.SignInAsync(
                    scheme: "LoginAuthen",
                    principal: principal,
                    properties: new AuthenticationProperties
                    {
                        //IsPersistent = true, // for 'remember me' feature
                        //ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                    });
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                    scheme: "LoginAuthen");
            HttpContext.Session.Clear();
            return RedirectToAction("FormLogin");
        }

        public IActionResult Access()
        {
            return View();
        }

        private void SetSessionValue(Users info)
        {
            NLogLogger.LogInfo("Set Session Info");
            HttpContext.Session.SetBoolean(SessionState.SESSION_ISADMINISTRATOR, info.IsAdministrator);
            HttpContext.Session.SetBoolean(SessionState.SESSION_STATUS, info.Status);
        }
    }
}

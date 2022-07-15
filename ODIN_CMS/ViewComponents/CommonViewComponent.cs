using Microsoft.AspNetCore.Mvc;
using Utils;
using Microsoft.AspNetCore.Http;
using DataAccess.Factory;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using ODIN_CMS.Controllers.Common;
using System.Threading.Tasks;

namespace ODIN_CMS.ViewComponents
{

    public class CommonViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.Delay(1);
            return View();
        }
    }

    [ViewComponent(Name = "Menu")]
    public class MenuViewComponent : ViewComponent
    {
        private UserFunction Permission { get { return HttpContext.Session.GetObject<UserFunction>(SessionState.SESSION_PERMISSION); } }
        private CommonLib CommonLib = new CommonLib();
        public static bool IsAdministrator = false;
        private static string UserName = "";
        private static int UserID = 0;
        private static int GroupID = 0;
        private static int MerchantID = 1;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.Delay(1);
            IsAdministrator = IdentityHelper.IsAdministrator;
            UserID = IdentityHelper.UserID;
            GroupID = IdentityHelper.GroupID;
            MerchantID = IdentityHelper.MerchantID;
            int type = 0;
            if (MerchantID > 1)
                type = 1;
            var urlLogin = Config.URL_ROOT + "Account/FormLogin";
            if (!User.Identity.IsAuthenticated || UserID == 0)
            {
                return View("FormLogin");
            }
            else
            {
                var functions = new List<GroupFunctions>();
                functions = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionGetList(GroupID, type);
                HttpContext.Session.SetObject(SessionState.SESSION_FUNCTIONS, functions);
                ViewBag.Functions = HttpContext.Session.GetObject<List<GroupFunctions>>(SessionState.SESSION_FUNCTIONS);
                ViewBag.IsAdmin = IsAdministrator;
                return View("Menu", functions);
            }
        }
    }
}

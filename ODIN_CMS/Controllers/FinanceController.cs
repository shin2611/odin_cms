using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ODIN_CMS.Models;
using Utils;
using Microsoft.AspNetCore.Http;
using DataAccess.Factory;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using Utils.Security;
using Newtonsoft.Json;
using ODIN_CMS.Controllers.Common;

namespace ODIN_CMS.Controllers
{
    public class FinanceController : Controller
    {
        private static int UserID = 0;
        private static int GroupID = 0;
        private static int MerchantID = 1;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FinanceManager()
        {
            UserID = IdentityHelper.UserID;
            GroupID = IdentityHelper.GroupID;
            MerchantID = IdentityHelper.MerchantID;
            var urlLogin = Config.URL_ROOT + "Account/FormLogin";
            if (!User.Identity.IsAuthenticated)
            {
                return new RedirectResult(urlLogin);
            }

            var data = AbstractDAOFactory.Instance().CreateCMSDAO().MerchantGetList(MerchantID);
            ViewBag.ListMerchant = data;
            return View();
        }

        public IActionResult TableListFinance(string keyword, string startTime, string endTime, int merchantId)
        {
            var data = new List<Finance>();
            UserID = IdentityHelper.UserID;
            GroupID = IdentityHelper.GroupID;
            MerchantID = IdentityHelper.MerchantID;

            int IdMerchant = merchantId == -1 ? MerchantID : (int)merchantId;

            var ToDay = DateTime.Now;

            if (string.IsNullOrEmpty(startTime))
                startTime = ToDay.AddDays(-6).ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(endTime))
                endTime = ToDay.ToString("yyyy-MM-dd");

            data = AbstractDAOFactory.Instance().CreateCMSDAO().FinanceGetList(keyword, startTime, endTime, IdMerchant);

            return PartialView("TableListFinance", data);
        }

        public IActionResult GetFinanceInfo(int? id)
        {
            UserID = IdentityHelper.UserID;
            GroupID = IdentityHelper.GroupID;
            MerchantID = IdentityHelper.MerchantID;
            ViewBag.MerchantID = 0;
            ViewBag.MerchantName = string.Empty;

            int FinanceId = id == null ? 0 : (int)id;
            var financeInfo = new Finance();
            var lstMerchant = new List<Merchant>();
            var model = new FinanceInfoViewModel();

            if (FinanceId > 0)
            {
                financeInfo = AbstractDAOFactory.Instance().CreateCMSDAO().FinanceGetInfoByID(FinanceId);
                ViewBag.MerchantID = financeInfo.MerchantID;
                ViewBag.MerchantName = financeInfo.MerchantName;
            }

            lstMerchant = AbstractDAOFactory.Instance().CreateCMSDAO().MerchantGetList(MerchantID);
            model.ListMerchant = lstMerchant;
            return PartialView("GetFinanceInfo", model);
        }
    }
}

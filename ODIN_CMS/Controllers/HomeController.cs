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
    public class HomeController : Controller
    {
        private UserFunction Permission { get { return HttpContext.Session.GetObject<UserFunction>(SessionState.SESSION_PERMISSION); } }
        private CommonLib CommonLib = new CommonLib();
        public static bool IsAdministrator = false;
        private static string UserName = "";
        private static int UserID = 0;
        private static int GroupID = 0;
        private static int MerchantID = 1;
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("FormLogin", "Account");
            }
            return View();
        }

        #region COMMON
        public IActionResult Menu()
        {
            IsAdministrator = IdentityHelper.IsAdministrator;
            UserID = IdentityHelper.UserID;
            GroupID = IdentityHelper.GroupID;
            int type = 0;
            if (IdentityHelper.MerchantID > 1)
                type = 1;
            var urlLogin = Config.URL_ROOT + "Account/FormLogin";
            if (!User.Identity.IsAuthenticated || UserID == 0)
            {
                return new RedirectResult(urlLogin);
            }
            else
            {
                var functions = new List<GroupFunctions>();
                functions = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionGetList(GroupID, type);
                HttpContext.Session.SetObject(SessionState.SESSION_FUNCTIONS, functions);
                ViewBag.Functions = HttpContext.Session.GetObject<List<GroupFunctions>>(SessionState.SESSION_FUNCTIONS);
                ViewBag.IsAdmin = IsAdministrator;
                return PartialView("Menu", functions);
            }
        }
        public IActionResult Header()
        {
            UserID = IdentityHelper.UserID;
            var urlLogin = Config.URL_ROOT + "Account/FormLogin";
            if (!User.Identity.IsAuthenticated)
            {
                return new RedirectResult(urlLogin);
            }
            ViewBag.UserName = IdentityHelper.Username;
            ViewBag.LogIn = true;
            ViewBag.UrlLogin = urlLogin;
            return PartialView();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ErrorPermission()
        {
            return PartialView();
        }
        public IActionResult ErrorNotPage()
        {
            return PartialView();
        }
        public IActionResult ErrorInternalServer()
        {
            return PartialView();
        }
        public static string GetChildMenu(GroupFunctions func, List<GroupFunctions> listChild)
        {
            var ListChild = listChild.FindAll(f => f.ParentID == func.FunctionID && f.IsDisplay == true);
            ListChild.Sort((f1, f2) => f1.Order.CompareTo(f2.Order));
            var script = "";
            if (ListChild.Count > 0)
            {
                script += "<li class=\"treeview \">" +
                        "<a href=\"javascript:void(0);\">" +
                        "<i class=\"" + func.CssIcon + "\"></i>" +
                        "<span class=\"title\">" + func.FunctionName + "</span>" +
                        "<span class=\"pull-right-container\">" +
                        "<i class=\"fa fa-angle-right pull-right\"></i></span>";
                script += "<ul class=\"treeview-menu\">";
                foreach (var obj in ListChild)
                {
                    script += GetChildMenu(obj, listChild);
                }
                script += "</ul></li>";
            }
            else
            {
                script += "<li><a href=\"" + Config.URL_ROOT + func.Url + "\">" +
                    "<i class=\"" + func.CssIcon + "\"></i>" +
                    "<span class=\"title\">" + func.FunctionName + "</span></a></li>";
            }
            return script;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion

        #region QUẢN TRỊ NGƯỜI DÙNG
        public IActionResult ManagerUser()
        {
            UserID = IdentityHelper.UserID;
            GroupID = IdentityHelper.GroupID;
            MerchantID = IdentityHelper.MerchantID;
            var urlLogin = Config.URL_ROOT + "Account/FormLogin";
            if (!User.Identity.IsAuthenticated)
            {
                return new RedirectResult(urlLogin);
            }

            var data = AbstractDAOFactory.Instance().CreateFunctionDAO().GetListGroups(GroupID);
            var lstMerchant = AbstractDAOFactory.Instance().CreateCMSDAO().MerchantGetList(MerchantID);
            ViewBag.IsAdmin = IsAdministrator;
            ViewBag.GroupID = GroupID;
            ViewBag.ListGroup = data;
            ViewBag.ListMerchant = lstMerchant;
            return View();
        }

        public IActionResult ListUsers(int? isActive, int? groupId, int? merchantId)
        {
            var keyword = String.Empty;
            var data = new List<Users>();
            UserID = IdentityHelper.UserID;
            GroupID = IdentityHelper.GroupID;
            MerchantID = IdentityHelper.MerchantID;

            int IsActive = isActive == null ? -1 : (int)isActive;
            int IdGroup = groupId == -1 ? GroupID : (int)groupId;
            int IdMerchant = merchantId == -1 ? MerchantID : (int)merchantId;

            data = AbstractDAOFactory.Instance().CreateUsersDAO().GetListUsers(keyword, IsActive, IdGroup, IdMerchant);

            return PartialView("ListUsers", data);
        }

        public IActionResult GetUserInfo(int? id)
        {
            UserID = IdentityHelper.UserID;
            GroupID = IdentityHelper.GroupID;
            MerchantID = IdentityHelper.MerchantID;
            ViewBag.GroupID = 0;
            ViewBag.GroupName = string.Empty;
            ViewBag.MerchantID = 0;
            ViewBag.MerchantName = string.Empty;

            int UserId = id == null ? 0 : (int)id;
            var userInfo = new Users();
            var lstGroup = new List<Groups>();
            var lstMerchant = new List<Merchant>();
            var model = new UserInfoViewModel();

            if (UserId > 0)
            {
                userInfo = AbstractDAOFactory.Instance().CreateUsersDAO().SelectByUserID(UserId);
                ViewBag.GroupID = userInfo.GroupID;
                ViewBag.GroupName = userInfo.GroupName;
                ViewBag.MerchantID = userInfo.MerchantID;
                ViewBag.MerchantName = userInfo.MerchantName;
            }
            lstGroup = AbstractDAOFactory.Instance().CreateFunctionDAO().GetListGroups(GroupID);
            lstMerchant = AbstractDAOFactory.Instance().CreateCMSDAO().MerchantGetList(MerchantID);
            ViewBag.AdGroupID = GroupID;
            ViewBag.AdMerchantID = MerchantID;
            model.Users = userInfo;
            model.ListGroups = lstGroup;
            model.ListMerchant = lstMerchant;
            return PartialView("GetUserInfo", model);
        }

        public IActionResult GetGrantUserInfo(int id)
        {
            UserID = IdentityHelper.UserID;
            var listFunction = new List<FunctionsGrant>();
            var _ListFunction = new List<Functions>();
            var listtree = new List<TreeDataFunction>();
            var userGrant = AbstractDAOFactory.Instance().CreateUsersDAO().SelectByUserID(id);
            ViewBag.User = userGrant.Username + " (" + userGrant.FullName + ")";
            ViewBag.IsAdmin = IsAdministrator ? 1 : 0;
            ViewBag.UserId = id;
            int FunctionSystem = 1;
            //Lấy quyền hiện tại của người cần cấp
            var userfunction = AbstractDAOFactory.Instance().CreateUserRoleDAO().UserFunction_GetByUserID(id);
            if (IsAdministrator)
            {
                _ListFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().SelectAllFunctionID(-1, string.Empty, -1, -1);
                foreach (var f in _ListFunction)
                {
                    listFunction.Add(new FunctionsGrant
                    {
                        FunctionID = f.FunctionID,
                        ParentID = f.ParentID,
                        FunctionName = f.FunctionName,
                        FatherName = f.FatherName,
                        IsReport = f.IsReport,
                        CssIcon = f.CssIcon
                    });
                }
            }
            //Là user mod -> lọc tất cả các chức năng báo cáo của mod 
            else if (Permission.IsGrant)
            {
                var checkMod = userfunction.FindAll(uf => uf.IsGrant == true);
                if (checkMod.Count > 0)
                {
                    return View(listtree);
                }
                var userMod = AbstractDAOFactory.Instance().CreateUsersDAO().SelectByUserID(UserID);

                //Lấy danh sách chức năng mà user có quyền mod
                var lst = AbstractDAOFactory.Instance().CreateFunctionDAO().GetListFunctionByUserID(UserID, 1);
                foreach (var f in lst)
                {
                    if (f.FunctionID != FunctionSystem && f.ParentID != FunctionSystem)
                        listFunction.Add(new FunctionsGrant
                        {
                            FunctionID = f.FunctionID,
                            ParentID = f.ParentID,
                            FunctionName = f.FunctionName,
                            FatherName = f.FatherName,
                            IsReport = f.IsReport,
                            CssIcon = f.CssIcon
                        });
                }
            }

            if (listFunction != null && listFunction.Count > 0)
            {
                var level0 = new List<FunctionsGrant>();
                level0 = listFunction.FindAll(c => c.ParentID == 0);
                foreach (var t in level0)
                {
                    var data = new TreeDataFunction
                    {
                        FunctionID = t.FunctionID,
                        FunctionName = t.FunctionName,
                        CssIcon = t.CssIcon,
                        ParentID = t.ParentID,
                        IsReport = t.IsReport,
                        IsView = false
                    };
                    if (userfunction != null && userfunction.Count > 0)
                    {
                        var fct = userfunction.Find(uf => uf.FunctionID == t.FunctionID);
                        if (fct != null)
                        {
                            data.IsView = true;
                            data.IsInsert = fct.IsInsert;
                            data.IsUpdate = fct.IsUpdate;
                            data.IsDelete = fct.IsDelete;
                            data.IsGrant = fct.IsGrant;
                        }
                    }
                    listtree.Add(data);
                    var chils = this.GetListTreeFunction(t.FunctionID, listFunction, userfunction);
                    data.Childrent = chils;
                }
            }
            ViewBag.Permission = Permission;
            return View("GetGrantUserInfo", listtree);
        }

        public IActionResult ListUserFunction(int FunctionID)
        {
            var ListUserFunction = new List<UserFunction>();
            var ListUserRole = new List<UserFunction>();
            var UserID = IdentityHelper.UserID;
            if (HttpContext.Session.GetBoolean(SessionState.SESSION_ISADMINISTRATOR) != null)
            {
                IsAdministrator = Convert.ToBoolean(HttpContext.Session.GetBoolean(SessionState.SESSION_ISADMINISTRATOR));
            }
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("FormLogin");
            }
            ViewBag.IsAdmin = IsAdministrator;
            var checkPermission = CheckPermissionUser("ManagerUser", UserID, IsAdministrator);
            var Permission = HttpContext.Session.GetObject<UserFunction>(SessionState.SESSION_PERMISSION);
            if (!checkPermission || Permission == null)
            {
                return new RedirectResult(Config.URL_ROOT + "Home/ErrorPermission", true);
            }

            var m_function = AbstractDAOFactory.Instance()
                .CreateFunctionDAO().GetFunctionByFunctionID(FunctionID);
            if (m_function == null || m_function.FunctionID <= 0)
                return PartialView(ListUserFunction);
            var m_permission = new UserFunction();

            //Nếu user là admin -> full quyền
            if (IsAdministrator)
            {
                m_permission.FunctionID = FunctionID;
                m_permission.UserID = UserID;
                m_permission.IsGrant = true;
                m_permission.IsInsert = true;
                m_permission.IsUpdate = true;
                m_permission.IsDelete = true;
            }
            else
            {
                //Lấy quyền của user khi truy cập chức năng
                var roleFuncID = AbstractDAOFactory.Instance().CreateUserRoleDAO().
                    CheckPermission(UserID, m_function.ActionName);
                if (roleFuncID == null || roleFuncID.FunctionID == 0)
                    return PartialView(ListUserFunction);

                //Kiểm tra có quyền truy cập chức năng phân quyền người dùng ko ?
                m_permission = AbstractDAOFactory.Instance().CreateUserRoleDAO().
                    CheckPermission(UserID, "GetGrantUserInfo");
                if (m_permission == null || m_permission.FunctionID == 0)
                    return PartialView(ListUserFunction);
            }
            int IsGrant = 2; //Lấy toàn bộ User có quyền
            //Lấy tất cả User đang hoạt động -> chỉ phân quyền cho User đang hoạt động
            var ListAllUser = AbstractDAOFactory.Instance().CreateUsersDAO().GetAllUserByStatus(1);
            //Lọc những tk không phải là admin
            var ListUserFilter = ListAllUser.FindAll(us => us.IsAdministrator == false);
            //Lấy tất cả User có quyền tương ứng vs chức năng hiện tại
            ListUserRole = AbstractDAOFactory.Instance().CreateUserRoleDAO().GetListUser_ByRoleFunctionID(FunctionID, IsGrant);

            if (IsAdministrator)
            {
                foreach (var us in ListUserFilter)
                {
                    var m_userfunction = new UserFunction
                    {
                        UserID = us.UserID,
                        UserName = us.Username,
                        FunctionID = FunctionID
                    };
                    //Tìm user có quyền
                    var findUser = ListUserRole.Find(ur => ur.UserID == us.UserID);
                    if (findUser != null && findUser.UserID > 0)
                    {
                        m_userfunction.IsRead = true; //xác định có quyền
                        m_userfunction.IsGrant = findUser.IsGrant;
                        m_userfunction.IsInsert = findUser.IsInsert;
                        m_userfunction.IsUpdate = findUser.IsUpdate;
                        m_userfunction.IsDelete = findUser.IsDelete;
                    }
                    ListUserFunction.Add(m_userfunction);
                }
            }
            else if (m_permission.IsGrant && !IsAdministrator)
            {
                foreach (var us in ListUserFilter)
                {
                    var m_userfunction = new UserFunction
                    {
                        UserID = us.UserID,
                        UserName = us.Username,
                        FunctionID = FunctionID
                    };
                    //Tìm user có quyền
                    var findUser = ListUserRole.Find(ur => ur.UserID == us.UserID);
                    if (findUser != null && findUser.UserID > 0)
                    {
                        if (!findUser.IsGrant)
                        {
                            m_userfunction.IsRead = true; //xác định có quyền
                            m_userfunction.IsGrant = findUser.IsGrant;
                            m_userfunction.IsInsert = findUser.IsInsert;
                            m_userfunction.IsUpdate = findUser.IsUpdate;
                            m_userfunction.IsDelete = findUser.IsDelete;
                        }
                        else
                            continue;
                    }
                    ListUserFunction.Add(m_userfunction);
                }
            }

            ViewBag.FunctionID = FunctionID;
            ViewBag.permission = m_permission;
            ViewBag.FunctionName = m_function.FunctionName;
            ViewBag.IsAdmin = IsAdministrator;
            return PartialView(ListUserFunction);
        }

        [HttpPost]
        public JsonResult SaveGrantForListUser(List<UserFunction> listUserFunction)
        {
            var ReturnData = new ReturnData();
            var m_permission = new UserFunction();
            var UserID = IdentityHelper.UserID;
            if (HttpContext.Session.GetBoolean(SessionState.SESSION_ISADMINISTRATOR) != null)
            {
                IsAdministrator = Convert.ToBoolean(HttpContext.Session.GetBoolean(SessionState.SESSION_ISADMINISTRATOR));
            }
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    var url = Config.URL_ROOT + "Account/FormLogin";
                    var urlLogout = string.Format("{0}?act=out", url);
                    Response.Redirect(urlLogout, true);
                }
                //Nếu user ko là admin 
                if (!IsAdministrator)
                {
                    //Kiểm tra có quyền truy cập chức năng phân quyền người dùng ko ?
                    m_permission = AbstractDAOFactory.Instance().CreateUserRoleDAO().
                         CheckPermission(UserID, "GetGrantUserInfo");
                    var Permission = HttpContext.Session.GetObject<UserFunction>(SessionState.SESSION_PERMISSION);
                    if (m_permission == null || m_permission.FunctionID == 0 || !Permission.IsInsert || !Permission.IsUpdate || !Permission.IsDelete)
                    {
                        ReturnData.ResponseCode = -101;
                        ReturnData.ResponseDesc = "Bạn không có quyền sử dụng chức năng này";
                        return Json(ReturnData);
                    }
                }

                if (listUserFunction == null || listUserFunction.Count == 0)
                {
                    ReturnData.ResponseCode = -7001;
                    ReturnData.ResponseDesc = "Bạn chưa chọn quyền cho user";
                    return Json(ReturnData);
                }

                var m_function = AbstractDAOFactory.Instance().CreateFunctionDAO().GetFunctionByFunctionID(listUserFunction[0].FunctionID);
                if (m_function == null || m_function.FunctionID <= 0)
                {
                    ReturnData.ResponseCode = -7002;
                    ReturnData.ResponseDesc = "Chức năng phân quyền ko tồn tại!";
                    return Json(ReturnData);
                }
                string ListRole = string.Empty;

                foreach (var userfunction in listUserFunction)
                {
                    if (userfunction.IsRead)
                        ListRole += userfunction.UserID + "," + userfunction.IsInsert + "," + userfunction.IsUpdate + "," + userfunction.IsDelete + ";";
                    else
                    {
                        AbstractDAOFactory.Instance().CreateUserRoleDAO().UserFunctionDelete(userfunction.UserID, userfunction.FunctionID);
                    }
                }
                if (string.IsNullOrEmpty(ListRole))
                {
                    ReturnData.ResponseCode = 1;
                    ReturnData.ResponseDesc = "Phân quyền thành công";
                    return Json(ReturnData);
                }

                if (!string.IsNullOrEmpty(ListRole))
                {
                    ListRole = ListRole.Substring(0, ListRole.Length - 1);
                }
                var Result = AbstractDAOFactory.Instance().CreateUserRoleDAO().UserFunctionInsertListByFunctionID(listUserFunction[0].FunctionID, ListRole, UserID);
                if (Result >= 0)
                {
                    ReturnData.ResponseDesc = "Phân quyền Thành Công";
                    //Ghi log
                    //AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    //{
                    //    FunctionID = Permission.FunctionID,
                    //    FunctionName = Permission.FunctionName,
                    //    Description = "phân quyền chức năng " + m_function.FunctionName + " với danh sách TK (" + ListRole + ")",
                    //    UserID = userValidate.UserId,
                    //    ClientIP = userValidate.ClientIP
                    //});
                }
                else if (Result == -101)
                    ReturnData.ResponseDesc = "Chức năng không tồn tại";
                else if (Result == -600)
                    ReturnData.ResponseDesc = "Tham số truyền vào không hợp lệ";
                else
                    ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau !";

                ReturnData.ResponseCode = Result;

                return Json(ReturnData);
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex);
                ReturnData.ResponseCode = -99;
                ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(ReturnData);
            }
        }
        public List<TreeDataFunction> GetListTreeFunction(int parentId, List<FunctionsGrant> roots, List<UserFunction> lt1)
        {
            var tmp = new List<TreeDataFunction>();
            var levesub = roots.FindAll(c => c.ParentID == parentId);
            if (levesub.Count <= 0) return tmp;
            foreach (var t in levesub)
            {
                var data = new TreeDataFunction
                {
                    FunctionID = t.FunctionID,
                    ParentID = t.ParentID,
                    FunctionName = t.FunctionName,
                    CssIcon = t.CssIcon,
                    IsReport = t.IsReport,
                    IsView = false
                };
                if (lt1 != null && lt1.Count > 0)
                {
                    var fct = lt1.Find(uf => uf.FunctionID == t.FunctionID);
                    if (fct != null)
                    {
                        data.IsView = true;
                        data.IsInsert = fct.IsInsert;
                        data.IsUpdate = fct.IsUpdate;
                        data.IsDelete = fct.IsDelete;
                        data.IsGrant = fct.IsGrant;
                    }
                }
                tmp.Add(data);
                var childrens = this.GetListTreeFunction(t.FunctionID, roots, lt1);
                data.Childrent = childrens;
            }

            return tmp;
        }

        public List<TreeDataFunction> GetListTreeGroupFunction(int parentId, List<FunctionsGrant> roots, List<GroupFunctions> lt1)
        {
            var tmp = new List<TreeDataFunction>();
            var levesub = roots.FindAll(c => c.ParentID == parentId);
            if (levesub.Count <= 0) return tmp;
            foreach (var t in levesub)
            {
                var data = new TreeDataFunction
                {
                    FunctionID = t.FunctionID,
                    ParentID = t.ParentID,
                    FunctionName = t.FunctionName,
                    CssIcon = t.CssIcon,
                    IsReport = t.IsReport,
                    IsView = false,
                    Url = t.Url
                };
                if (lt1 != null && lt1.Count > 0)
                {
                    var fct = lt1.Find(uf => uf.FunctionID == t.FunctionID);
                    if (fct != null)
                    {
                        data.IsGrant = fct.IsGrant;
                        data.IsView = fct.IsView;
                        data.IsInsert = fct.IsInsert;
                        data.IsUpdate = fct.IsUpdate;
                        data.IsDelete = fct.IsDelete;
                        data.IsDisplay = fct.IsDisplay;
                    }
                }
                tmp.Add(data);
                var childrens = this.GetListTreeGroupFunction(t.FunctionID, roots, lt1);
                data.Childrent = childrens;
            }

            return tmp;
        }


        public static string GetChildUserFunctionGrant(List<TreeDataFunction> ListChildFunction)
        {
            GroupID = IdentityHelper.GroupID;
            var script = string.Empty;
            if (ListChildFunction != null && ListChildFunction.Count > 0)
            {
                foreach (var usergrant in ListChildFunction)
                {
                    if (usergrant.ParentID > 0)
                    {

                        if (usergrant.Childrent.Count == 0)
                        {
                            if (GroupID <= 2)
                            {
                                script += "<tr class=\"treegrid-" + usergrant.FunctionID + " treegrid-parent-" + usergrant.ParentID + "\"><td><i class=\"" + usergrant.CssIcon + "\"></i>" +
                                        usergrant.FunctionName + "</td>";
                                script += "<td>" + usergrant.Url + "</td>";

                                script += "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"IsGrant CheckGrant form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsGrant == true) ? "checked" : "") + " /></td>";
                                script += "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"IsView CheckView form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsView == true) ? "checked" : "") + " /></td>" +
                                "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"IsInsert CheckInsert form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsInsert == true) ? "checked" : "") + " /></td>" +
                                "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"IsUpdate CheckUpdate form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsUpdate == true) ? "checked" : "") + " /></td>" +
                                "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"IsDelete CheckDelete form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsDelete == true) ? "checked" : "") + " /></td>";
                                if (GroupID == 1)
                                {
                                    script += "<td valign=\"middle\" align=\"center\"><a class=\"btn btn-success\" style=\"margin:5px;\" href=\"javascript:;\" onclick=\"ViewFunctionData(" + usergrant.FunctionID + ");\">Thêm menu</a><a class=\"btn btn-warning\" href=\"javascript:;\" onclick=\"ViewFunctionData(" + usergrant.FunctionID + ");\">Sửa</a><a class=\"btn btn-danger\" style=\"margin:5px;\" href=\"javascript:;\" onclick=\"RemoveMenu(" + usergrant.FunctionID + ");\">Xóa</a></td>";
                                }

                                script += "</tr>";
                            }

                        }
                        else
                        {
                            script += "<tr class=\"treegrid-" + usergrant.FunctionID + " treegrid-parent-" + usergrant.ParentID + "\"><td><i class=\"" + usergrant.CssIcon + "\"></i>" +
                                        usergrant.FunctionName + "</td>";
                            script += "<td>" + usergrant.Url + "</td>";

                            script += "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"CheckAllGrant CheckGrant form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsGrant == true) ? "checked" : "") + " /></td>";
                            script += "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"CheckAllView CheckView form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsView == true) ? "checked" : "") + " /></td>" +
                            "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"CheckAllInsert CheckInsert form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsInsert == true) ? "checked" : "") + " /></td>" +
                            "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"CheckAllUpdate CheckUpdate form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsUpdate == true) ? "checked" : "") + "/></td>" +
                            "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"CheckAllDelete CheckDelete form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsDelete == true) ? "checked" : "") + "/></td>";
                            if (GroupID == 1)
                            {
                                script += "<td valign=\"middle\" align=\"center\"><a class=\"btn btn-success\" style=\"margin:5px;\" href=\"javascript:;\" onclick=\"ViewFunctionData(" + usergrant.FunctionID + ");\">Thêm menu</a><a class=\"btn btn-warning\" href=\"javascript:;\" onclick=\"ViewFunctionData(" + usergrant.FunctionID + ");\">Sửa</a><a class=\"btn btn-danger\" style=\"margin:5px;\" href=\"javascript:;\" onclick=\"RemoveMenu(" + usergrant.FunctionID + ");\">Xóa</a></td>";
                            }
                            script += "</tr>";

                        }

                    }
                    else
                    {
                        script += "<tr class=\"treegrid-" + usergrant.FunctionID + "\"><td><i class=\"" + usergrant.CssIcon + "\"></i>" +
                        usergrant.FunctionName + "</td>";
                        script += "<td>" + usergrant.Url + "</td>";

                        script += "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"CheckAllGrant CheckGrant form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsGrant == true) ? "checked" : "") + " /></td>";
                        script += "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"CheckAllView CheckView form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsView == true) ? "checked" : "") + "/></td>" +
                        "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"CheckAllInsert CheckInsert form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsInsert == true) ? "checked" : "") + "/></td>" +
                        "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"CheckAllUpdate CheckUpdate form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsUpdate == true) ? "checked" : "") + "/></td>" +
                        "<td valign=\"middle\" align=\"center\"><input style=\"opacity:1;left: auto;\" class=\"CheckAllDelete CheckDelete form-check-input form-check-primary\" id=\"" + usergrant.FunctionID + "\" type=checkbox " + ((usergrant.IsDelete == true) ? "checked" : "") + "/></td>";
                        if (GroupID == 1)
                        {
                            script += "<td valign=\"middle\" align=\"center\"><a class=\"btn btn-success\" style=\"margin:5px;\" href=\"javascript:;\" onclick=\"ViewFunctionData(" + usergrant.FunctionID + ");\">Thêm menu</a><a class=\"btn btn-warning\" href=\"javascript:;\" onclick=\"ViewFunctionData(" + usergrant.FunctionID + ");\">Sửa</a><a class=\"btn btn-danger\" style=\"margin:5px;\" href=\"javascript:;\" onclick=\"RemoveMenu(" + usergrant.FunctionID + ");\">Xóa</a></td>";
                        }
                    }

                    if (usergrant.Childrent.Count > 0)
                        script += GetChildUserFunctionGrant(usergrant.Childrent);
                }
                return script;
            }
            else
                return script;
        }

        [HttpPost]
        public JsonResult SaveDataUser(Users users)
        {
            var ReturnData = new ReturnData();
            UserID = IdentityHelper.UserID;
            UserName = IdentityHelper.Username;
            GroupID = IdentityHelper.GroupID;
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    var url = Config.URL_ROOT + "Account/FormLogin";
                    var urlLogout = string.Format("{0}?act=out", url);
                    Response.Redirect(urlLogout, true);
                }
                var groupFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionDetail("ManagerUser", GroupID);

                if (groupFunction == null || (users.UserID == 0 && !groupFunction.IsInsert) || users.UserID > 0 && !groupFunction.IsUpdate)
                {
                    ReturnData.ResponseCode = -101;
                    ReturnData.ResponseDesc = "Bạn không có quyền sử dụng chức năng này";
                    return Json(ReturnData);
                }
                if (string.IsNullOrEmpty(users.Username))
                {
                    ReturnData.ResponseCode = -6001;
                    ReturnData.ResponseDesc = "Bạn chưa nhập tên tài khoản";
                    return Json(ReturnData);
                }
                if (string.IsNullOrEmpty(users.Email) || !users.Email.Contains("@"))
                {
                    ReturnData.ResponseCode = -6002;
                    ReturnData.ResponseDesc = "Email chưa nhập hoặc sai định dạng";
                    return Json(ReturnData);
                }
                users.FullName = string.IsNullOrEmpty(users.FullName) ? string.Empty : users.FullName;
                users.Password = string.IsNullOrEmpty(users.Password) ? string.Empty : Encrypt.MD5(users.Password);
                users.UserID = users.UserID == null ? 0 : users.UserID;
                users.MerchantID = users.MerchantID == null ? IdentityHelper.MerchantID : users.MerchantID;
                users.CreatedUser = UserName;
                var result = AbstractDAOFactory.Instance().CreateUsersDAO().UpdateUsers(users);
                ReturnData.ResponseCode = result;
                if (result >= 0)
                {
                    ReturnData.ResponseDesc = users.UserID > 0 ? "Cập nhật Thành Công" : "Thêm mới Thành Công";
                }
                else switch (result)
                    {
                        case -51: ReturnData.ResponseDesc = "Tài khoản đã tồn tại"; break;
                        case -52: ReturnData.ResponseDesc = "Email đã tồn tại"; break;
                        case -600: ReturnData.ResponseDesc = "Tham số truyền vào không hợp lệ"; break;
                        default: ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                    }
                return Json(ReturnData);
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex);
                ReturnData.ResponseCode = -99;
                ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(ReturnData);
            }
        }
        [HttpPost]
        public JsonResult SaveGrantUser(List<AddGrantFunction> ListAddGrant, List<DeleteGrantFunction> ListDelGrant, int GroupId)
        {
            var ReturnData = new ReturnData();
            UserID = IdentityHelper.UserID;
            GroupID = IdentityHelper.GroupID;
            int type = 0;
            if (GroupID > 2)
                type = 1;
            try
            {
                var Result = -99;
                if (!User.Identity.IsAuthenticated)
                {
                    var url = Config.URL_ROOT + "Account/FormLogin";
                    var urlLogout = string.Format("{0}?act=out", url);
                    Response.Redirect(urlLogout, true);
                }
                var groupFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionDetail("ManagerFunction", GroupID);

                if (groupFunction == null || (GroupId == 0 && !groupFunction.IsInsert) || GroupId > 0 && !groupFunction.IsUpdate)
                {
                    ReturnData.ResponseCode = -101;
                    ReturnData.ResponseDesc = "Bạn không có quyền sử dụng chức năng này";
                    return Json(ReturnData);
                }

                var m_group = AbstractDAOFactory.Instance().CreateFunctionDAO().GetInfo(GroupId);
                if (m_group == null || m_group.GroupID <= 0)
                {
                    ReturnData.ResponseCode = -102;
                    ReturnData.ResponseDesc = "Nhóm tài khoản phân quyền không tồn tại";
                    return Json(ReturnData);
                }
                string ListRole = string.Empty;
                string ListDel = string.Empty;
                if (ListDelGrant != null && ListDelGrant.Count > 0)
                {
                    foreach (var Grant in ListDelGrant)
                    {
                        ListDel += Grant.FunctionId + ",";
                    }
                }
                if (ListAddGrant != null && ListAddGrant.Count > 0)
                {
                    foreach (var Grant in ListAddGrant)
                    {
                        ListRole += Grant.FunctionId + "," + Grant.IsGrant + "," + Grant.IsView + "," + Grant.IsInsert + "," + Grant.IsUpdate + "," + Grant.IsDelete + ";";
                    }
                }

                //IF quyền được thêm không rỗng
                if (!string.IsNullOrEmpty(ListRole))
                {
                    if (IdentityHelper.GroupID <= 3 && groupFunction.IsGrant)
                    {
                        if (!string.IsNullOrEmpty(ListDel))
                        {
                            ListDel = ListDel.Substring(0, ListDel.Length - 1);
                            Result = AbstractDAOFactory.Instance().CreateUserRoleDAO().GroupFunctionDeleteList(GroupId, ListDel);
                        }
                    }
                    ListRole = ListRole.Substring(0, ListRole.Length - 1);
                    Result = AbstractDAOFactory.Instance().CreateUserRoleDAO().GroupFunctionInsertList(GroupId, ListRole, type);
                }
                else
                {
                    if (IdentityHelper.GroupID <= 3 && groupFunction.IsGrant)
                    {
                        if (!string.IsNullOrEmpty(ListDel))
                        {
                            ListDel = ListDel.Substring(0, ListDel.Length - 1);
                            Result = AbstractDAOFactory.Instance().CreateUserRoleDAO().GroupFunctionDeleteList(GroupId, ListDel);
                        }
                    }
                    else if (IdentityHelper.GroupID > 3)
                    {
                        Result = AbstractDAOFactory.Instance().CreateUserRoleDAO().GroupFunctionDeleteAll(GroupId);
                    }
                }

                if (Result >= 0)
                {
                    var role = string.Empty;
                    if (!string.IsNullOrEmpty(ListRole))
                    {
                        ReturnData.ResponseDesc = "Phân quyền Thành Công";
                        role = ListRole;
                    }
                    else
                    {
                        ReturnData.ResponseDesc = "Xóa quyền Thành Công";
                        role = ListDel;
                    }

                    //AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    //{
                    //    FunctionID = Permission.FunctionID,
                    //    FunctionName = Permission.FunctionName,
                    //    Description = ReturnData.Description + " tài khoản : " + m_user.Username,
                    //    UserID = userValidate.UserId,
                    //    ClientIP = userValidate.ClientIP
                    //});
                }
                else switch (Result)
                    {
                        case -50:
                            ReturnData.ResponseDesc = "Tài khoản không tồn tại";
                            break;
                        case -600:
                            ReturnData.ResponseDesc = "Tham số truyền vào không hợp lệ";
                            break;
                        default:
                            ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau";
                            break;
                    }
                ReturnData.ResponseCode = Result;

                return Json(ReturnData);
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex);
                ReturnData.ResponseCode = -99;
                ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(ReturnData);
            }
        }

        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            var ReturnData = new ReturnData();
            UserID = IdentityHelper.UserID;
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    var url = Config.URL_ROOT + "Account/FormLogin";
                    var urlLogout = string.Format("{0}?act=out", url);
                    Response.Redirect(urlLogout, true);
                }
                var groupFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionDetail("ManagerUser", GroupID);

                if (groupFunction == null || !groupFunction.IsInsert || !groupFunction.IsUpdate)
                {
                    ReturnData.ResponseCode = -101;
                    ReturnData.ResponseDesc = "Bạn không có quyền sử dụng chức năng này";
                    return Json(ReturnData);
                }
                if (id > 0)
                {
                    if (id == 14)
                    {
                        ReturnData.ResponseCode = -103;
                        ReturnData.ResponseDesc = "Bạn không đủ quyền để xóa tài khoản này";
                        return Json(ReturnData);
                    }
                    var m_user = AbstractDAOFactory.Instance().CreateUsersDAO().SelectByUserID(id);
                    if (m_user == null || m_user.UserID <= 0)
                    {
                        ReturnData.ResponseCode = -101;
                        ReturnData.ResponseDesc = "Không tồn tại tài khoản cần xóa";
                        return Json(ReturnData);
                    }
                    var result = AbstractDAOFactory.Instance().CreateUsersDAO().DelleteUsers(id);
                    if (result >= 0)
                    {
                        ReturnData.ResponseDesc = "Xóa user Thành Công";
                        //Ghi log
                        //AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                        //{
                        //    FunctionID = 9999,
                        //    FunctionName = "Xóa user",
                        //    Description = "Xóa thành công tài khoản " + m_user.Username,
                        //    UserID = userValidate.UserId,
                        //    ClientIP = userValidate.ClientIP
                        //});
                    }
                    else switch (result)
                        {
                            case -50: ReturnData.ResponseDesc = "Tài Khoản không tồn tại"; break;
                            case -99: ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                            default: ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                        }
                    return Json(ReturnData);
                }
                ReturnData.ResponseCode = -100;
                ReturnData.ResponseDesc = "Không xác định user cần xóa";
                return Json(ReturnData);
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex);
                ReturnData.ResponseCode = -99;
                ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(ReturnData);
            }
        }

        [HttpPost]
        public JsonResult UpdateActiveUser(int id)
        {
            var ReturnData = new ReturnData();
            UserID = IdentityHelper.UserID;
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    var url = Config.URL_ROOT + "Account/FormLogin";
                    var urlLogout = string.Format("{0}?act=out", url);
                    Response.Redirect(urlLogout, true);
                }
                var groupFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionDetail("ManagerUser", GroupID);

                if (groupFunction == null || !groupFunction.IsInsert || !groupFunction.IsUpdate)
                {
                    ReturnData.ResponseCode = -101;
                    ReturnData.ResponseDesc = "Bạn không có quyền sử dụng chức năng này";
                    return Json(ReturnData);
                }
                if (id >= 0)
                {
                    var m_user = AbstractDAOFactory.Instance().CreateUsersDAO().SelectByUserID(id);
                    if (m_user == null || m_user.UserID <= 0)
                    {
                        ReturnData.ResponseCode = -101;
                        ReturnData.ResponseDesc = "Không tồn tại tài khoản";
                        return Json(ReturnData);
                    }
                    var result = AbstractDAOFactory.Instance().CreateUsersDAO().UpdateActiveUser(id);
                    if (result >= 0)
                    {
                        //Ghi log
                        //AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                        //{
                        //    FunctionID = Permission.FunctionID,
                        //    FunctionName = Permission.FunctionName,
                        //    Description = "Cập nhật trạng thái tài khoản " + m_user.Username + " (Hệ Thống)",
                        //    UserID = userValidate.UserId,
                        //    ClientIP = userValidate.ClientIP
                        //});
                        ReturnData.ResponseDesc = "Cập nhật trạng thái Thành Công";
                    }
                    else switch (result)
                        {
                            case -50: ReturnData.ResponseDesc = "Tài Khoản không tồn tại"; break;
                            case -99: ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                            default: ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                        }
                    return Json(ReturnData);
                }
                ReturnData.ResponseCode = -100;
                ReturnData.ResponseDesc = "Không xác định user cần active";
                return Json(ReturnData);
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex);
                ReturnData.ResponseCode = -99;
                ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(ReturnData);
            }
        }
        #endregion

        #region Quản trị chức năng hệ thống
        public IActionResult ManagerFunction()
        {
            var groupFunction = new List<GroupFunctions>();
            GroupID = IdentityHelper.GroupID;
            if (!User.Identity.IsAuthenticated)
            {
                var url = Config.URL_ROOT + "Account/FormLogin";
                return new RedirectResult(url, true);
            }
            var data = AbstractDAOFactory.Instance().CreateFunctionDAO().GetListGroups(GroupID);

            ViewBag.ListGroups = data;
            ViewBag.GroupID = GroupID;
            return View();
        }

        public IActionResult ListGroupFunctions(int groupId)
        {
            var groupFunction = new List<GroupFunctions>();
            var listFunction = new List<FunctionsGrant>();
            var _ListFunction = new List<Functions>();
            var listtree = new List<TreeDataFunction>();
            GroupID = IdentityHelper.GroupID;
            UserID = IdentityHelper.UserID;

            if (!User.Identity.IsAuthenticated)
            {
                var url = Config.URL_ROOT + "Account/FormLogin";
                return new RedirectResult(url, true);
            }
            // Lấy quyền hiện tại theo nhóm
            int type = 0;
            if (IdentityHelper.MerchantID > 1)
                type = 1;
            groupFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionGetList(groupId, type);

            // Lấy danh sách chức năng hiện có
            _ListFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().SelectAllFunctionID(-1, string.Empty, -1, -1);
            foreach (var f in _ListFunction)
            {
                listFunction.Add(new FunctionsGrant
                {
                    FunctionID = f.FunctionID,
                    ParentID = f.ParentID,
                    FunctionName = f.FunctionName,
                    FatherName = f.FatherName,
                    IsReport = f.IsReport,
                    CssIcon = f.CssIcon,
                    IsDisplay = f.IsDisplay,
                    Url = f.Url
                });
            }

            if (listFunction != null && listFunction.Count > 0)
            {
                var level0 = new List<FunctionsGrant>();
                level0 = listFunction.FindAll(c => c.ParentID == 0);
                foreach (var t in level0)
                {
                    var data = new TreeDataFunction
                    {
                        FunctionID = t.FunctionID,
                        FunctionName = t.FunctionName,
                        CssIcon = t.CssIcon,
                        ParentID = t.ParentID,
                        IsReport = t.IsReport,
                        IsView = false,
                        IsGrant = t.IsGrant,
                        Url = t.Url
                    };
                    if (groupFunction != null && groupFunction.Count > 0)
                    {
                        var fct = groupFunction.Find(uf => uf.FunctionID == t.FunctionID);
                        if (fct != null)
                        {
                            data.IsView = fct.IsView;
                            data.IsGrant = fct.IsGrant;
                            data.IsInsert = fct.IsInsert;
                            data.IsUpdate = fct.IsUpdate;
                            data.IsDelete = fct.IsDelete;
                            data.IsDisplay = fct.IsDisplay;
                        }
                    }
                    listtree.Add(data);
                    var chils = this.GetListTreeGroupFunction(t.FunctionID, listFunction, groupFunction);
                    data.Childrent = chils;
                }
            }
            ViewBag.GroupID = GroupID;
            ViewBag.UserId = groupId;
            return PartialView("ListGroupFunctions", listtree);
        }

        public IActionResult GetFunctionInfo(int? id)
        {
            var result = new ModelFunctionDetail
            {
                FunctionDetail = new Functions(),
                ListFunction = new List<Functions>()
            };
            if (!User.Identity.IsAuthenticated)
            {
                var url = Config.URL_ROOT + "Account/FormLogin";
                return new RedirectResult(url, true);
            }
            var groupFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionDetail("ManagerFunction", GroupID);

            if (groupFunction == null || id == 0 && !groupFunction.IsInsert || id > 0 && !groupFunction.IsUpdate)
            {
                return new RedirectResult(Config.URL_ROOT + "Home/ErrorPermission", true);
            }
            else
            {
                int Id = id == null ? 0 : (int)id;
                result.ListFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().SelectAllFunctionID(-1, string.Empty, 1, -1);
                if (Id > 0)
                {
                    result.FunctionDetail = AbstractDAOFactory.Instance().CreateFunctionDAO().GetFunctionByFunctionID(Id);
                    if (result.FunctionDetail == null)
                        result.FunctionDetail = new Functions();
                }
            }
            return PartialView(result);
        }
        public IActionResult FunctionOrder()
        {
            var data = new List<Functions>();
            var UserID = IdentityHelper.UserID;
            if (HttpContext.Session.GetBoolean(SessionState.SESSION_ISADMINISTRATOR) != null)
            {
                IsAdministrator = Convert.ToBoolean(HttpContext.Session.GetBoolean(SessionState.SESSION_ISADMINISTRATOR));
            }
            if (!User.Identity.IsAuthenticated)
            {
                var url = Config.URL_ROOT + "Account/FormLogin";
                return new RedirectResult(url, true);
            }
            if (!IsAdministrator || Permission == null || Permission.ActionName != "ManagerFunction" || !Permission.IsUpdate)
            {
                return new RedirectResult(Config.URL_ROOT + "Home/ErrorPermission", true);
            }
            else
            {
                data = AbstractDAOFactory.Instance().CreateFunctionDAO().SelectAllFunctionID(-1, string.Empty, 1, -1);
            }
            ViewBag.IsAdmin = IsAdministrator;
            ViewBag.Permission = Permission;
            return View(data);
        }


        public static string GetChildFunction(int FatherID, List<Functions> ListFunction)
        {
            var function = ListFunction.Find(f => f.FunctionID == FatherID);
            var listChirl = ListFunction.FindAll(f => f.ParentID == FatherID);
            listChirl.Sort((f1, f2) => f1.Order.CompareTo(f2.Order));

            var script = "<li class=\"dd-item\" data-id=\"" + FatherID + "\"><div class=\"dd-handle\"><i class=\"" + function.CssIcon + "\" style=\"margin-right:7px\"></i>" + function.FunctionName + "</div>";

            if (listChirl.Count <= 0)
            {
                script += "</li>";
                return script;
            }
            script += "<ol class=\"dd-list\">";
            foreach (var t in listChirl)
            {
                script += GetChildFunction(t.FunctionID, ListFunction);
            }
            script += "</ol>";
            script += "</li>";
            return script;
        }

        [HttpPost]
        public JsonResult SaveDataFunction(Functions function)
        {
            var ReturnData = new ReturnData();
            if (!User.Identity.IsAuthenticated)
            {
                var url = Config.URL_ROOT + "Account/FormLogin";
                var urlLogout = string.Format("{0}?act=out", url);
                Response.Redirect(urlLogout, true);
            }
            UserName = IdentityHelper.Username;
            var groupFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionDetail("ManagerFunction", GroupID);

            if (groupFunction == null || function.FunctionID == 0 && !groupFunction.IsInsert || function.FunctionID > 0 && !groupFunction.IsUpdate)
            {
                ReturnData.ResponseCode = -101;
                ReturnData.ResponseDesc = "Bạn không có quyền sử dụng chức năng này";
                return Json(ReturnData);
            }
            if (string.IsNullOrEmpty(function.FunctionName))
            {
                ReturnData.ResponseCode = -100;
                ReturnData.ResponseDesc = "Bạn chưa nhập tên chức năng";
                return Json(ReturnData);
            }
            function.Url = string.IsNullOrEmpty(function.Url) ? string.Empty : function.Url;
            try
            {
                var result = AbstractDAOFactory.Instance().CreateFunctionDAO().InsertUpdateFunction(function);
                ReturnData.ResponseCode = result;
                if (result >= 0)
                {
                    string Description = "";
                    if (function.FunctionID > 0)
                    {
                        ReturnData.ResponseDesc = "Tài khoản : " + UserName + " Cập nhật";
                        Description = "cập nhật chức năng :" + function.FunctionName + " (Hệ Thống)";
                    }
                    else
                    {
                        ReturnData.ResponseDesc = "Tài khoản : " + UserName + " Thêm mới";
                        Description = "thêm mới chức năng :" + function.FunctionName + " (Hệ Thống)";
                    }
                    //Ghi log

                    //AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                    //{
                    //    FunctionID = Permission.FunctionID,
                    //    FunctionName = Permission.FunctionName,
                    //    Description = Description,
                    //    UserID = userValidate.UserId,
                    //    ClientIP = userValidate.ClientIP
                    //});
                }
                else switch (result)
                    {
                        case -70: ReturnData.ResponseDesc = "Tên chức năng đã tồn tại"; break;
                        case -71: ReturnData.ResponseDesc = "Đường dẫn đã tồn tại"; break;
                        case -99: ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                        default: ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                    }
                return Json(ReturnData);
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex);
                ReturnData.ResponseCode = -99;
                ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(ReturnData);
            }
        }

        [HttpPost]
        public JsonResult SaveOrderFunction(List<NestedOrder> listOrder)
        {
            var ReturnData = new ReturnData();
            if (!User.Identity.IsAuthenticated)
            {
                var url = Config.URL_ROOT + "Account/FormLogin";
                var urlLogout = string.Format("{0}?act=out", url);
                Response.Redirect(urlLogout, true);
            }
            if (HttpContext.Session.GetBoolean(SessionState.SESSION_ISADMINISTRATOR) != null)
            {
                IsAdministrator = Convert.ToBoolean(HttpContext.Session.GetBoolean(SessionState.SESSION_ISADMINISTRATOR));
            }
            if (!IsAdministrator || Permission == null || Permission.ActionName != "ManagerFunction" || !Permission.IsUpdate)
            {
                ReturnData.ResponseCode = -101;
                ReturnData.ResponseDesc = "Bạn không có quyền sử dụng chức năng này";
                return Json(ReturnData);
            }
            if (listOrder.Count <= 0)
            {
                ReturnData.ResponseCode = -100;
                ReturnData.ResponseDesc = "không tồn tại danh sách chức năng sắp xếp mới";
                return Json(ReturnData);
            }

            try
            {
                foreach (var t in listOrder)
                {
                    AbstractDAOFactory.Instance().CreateFunctionDAO().UpdateOrder(t.Id, t.FatherID, t.Order);
                }

                ReturnData.ResponseCode = 1;
                ReturnData.ResponseDesc = "Sắp xếp Thành Công Chức Năng";
                //Ghi log

                //AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                //{
                //    FunctionID = Permission.FunctionID,
                //    FunctionName = Permission.FunctionName,
                //    Description = "Sắp xếp chức năng (hệ thống)",
                //    UserID = userValidate.UserId,
                //    ClientIP = userValidate.ClientIP
                //});
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex);
                ReturnData.ResponseCode = -99;
                ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau";
            }
            return Json(ReturnData);
        }

        [HttpPost]
        public JsonResult DeleteFunction(int id)
        {
            var ReturnData = new ReturnData();
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    var url = Config.URL_ROOT + "Account/FormLogin";
                    var urlLogout = string.Format("{0}?act=out", url);
                    Response.Redirect(urlLogout, true);
                }
                var groupFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionDetail("ManagerFunction", GroupID);
                if (groupFunction == null || id == 0 && !groupFunction.IsDelete)
                {
                    ReturnData.ResponseCode = -101;
                    ReturnData.ResponseDesc = "Bạn không có quyền sử dụng chức năng này";
                    return Json(ReturnData);
                }
                if (id > 0)
                {
                    var Function = AbstractDAOFactory.Instance().CreateFunctionDAO().GetFunctionByFunctionID(id);
                    if (Function == null)
                    {
                        ReturnData.ResponseCode = -102;
                        ReturnData.ResponseDesc = "chức năng xóa không tồn tại !";
                        return Json(ReturnData);
                    }
                    var result = AbstractDAOFactory.Instance().CreateFunctionDAO().DelleteFunction(id);
                    ReturnData.ResponseCode = result;
                    if (result >= 0)
                    {
                        ReturnData.ResponseDesc = "Xóa chức năng Thành Công";
                        //Ghi log
                        //AbstractDAOFactory.Instance().CreateUsersLogDAO().InsertUsersLog(new UsersLog
                        //{
                        //    FunctionID = Permission.FunctionID,
                        //    FunctionName = Permission.FunctionName,
                        //    Description = "Xóa chức năng " + Function.FunctionName + " (Hệ thống)",
                        //    UserID = userValidate.UserId,
                        //    ClientIP = userValidate.ClientIP
                        //});
                    }
                    else switch (result)
                        {
                            case -24: ReturnData.ResponseDesc = "Chức năng đang trong trạng thái hoạt động. Hãy tắt trạng thái hoạt động của hệ thống trước khi xóa !"; break;
                            case -25: ReturnData.ResponseDesc = "Chức năng không tồn tại trong hệ thống"; break;
                            case -99: ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                            default: ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                        }
                    return Json(ReturnData);
                }
                ReturnData.ResponseCode = -100;
                ReturnData.ResponseDesc = "Không xác định chức năng cần xóa";
                return Json(ReturnData);
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex);
                ReturnData.ResponseCode = -99;
                ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(ReturnData);
            }
        }

        #endregion

        #region QUẢN TRỊ NHÓM TÀI KHOẢN

        public IActionResult ManagerGroup()
        {

            GroupID = IdentityHelper.GroupID;
            if (!User.Identity.IsAuthenticated)
            {
                var url = Config.URL_ROOT + "Account/FormLogin";
                return new RedirectResult(url, true);
            }
            var data = AbstractDAOFactory.Instance().CreateFunctionDAO().GetListGroups(GroupID);

            ViewBag.GroupID = GroupID;
            return View(data);
        }

        public IActionResult GetGroupInfo(int id)
        {
            UserID = IdentityHelper.UserID;
            GroupID = IdentityHelper.GroupID;
            ViewBag.GroupID = 0;
            ViewBag.GroupName = string.Empty;
            var groupInfo = new GroupEdit();
            int groupId = id == null ? 0 : (int)id;

            if (groupId > 0)
            {
                groupInfo = AbstractDAOFactory.Instance().CreateFunctionDAO().GetInfo(groupId);
                ViewBag.GroupID = groupInfo.GroupID;
                ViewBag.GroupName = groupInfo.Name;
            }
            ViewBag.AdGroupID = GroupID;
            return PartialView("GetGroupInfo", groupInfo);
        }

        [HttpPost]
        public JsonResult UpdateGroup(GroupEdit group)
        {
            var ReturnData = new ReturnData();
            UserID = IdentityHelper.UserID;
            UserName = IdentityHelper.Username;
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    var url = Config.URL_ROOT + "Account/FormLogin";
                    var urlLogout = string.Format("{0}?act=out", url);
                    Response.Redirect(urlLogout, true);
                }
                var groupFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionDetail("ManagerGroup", GroupID);

                if (groupFunction == null || (group.GroupID == 0 && !groupFunction.IsInsert) || group.GroupID > 0 && !groupFunction.IsUpdate)
                {
                    ReturnData.ResponseCode = -101;
                    ReturnData.ResponseDesc = "Bạn không có quyền sử dụng chức năng này";
                    return Json(ReturnData);
                }

                group.ExeUserID = UserID;
                group.CreatedBy = UserName;

                var result = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupEdit(group);

                ReturnData.ResponseCode = result;
                if (result >= 0)
                {
                    ReturnData.ResponseDesc = group.GroupID > 0 ? "Cập nhật Thành Công" : "Thêm mới Thành Công";
                }
                else switch (result)
                    {
                        case -51: ReturnData.ResponseDesc = "Chức năng không tồn tại trong hệ thống"; break;
                        case -99: ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                        default: ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                    }
                return Json(ReturnData);

            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex);
                ReturnData.ResponseCode = -99;
                ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(ReturnData);
            }
        }

        [HttpPost]
        public JsonResult DeleteGroup(int groupId)
        {
            var ReturnData = new ReturnData();
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    var url = Config.URL_ROOT + "Account/FormLogin";
                    var urlLogout = string.Format("{0}?act=out", url);
                    Response.Redirect(urlLogout, true);
                }
                var groupFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionDetail("ManagerGroup", GroupID);

                if (groupFunction == null || (groupId > 0 && !groupFunction.IsDelete))
                {
                    ReturnData.ResponseCode = -101;
                    ReturnData.ResponseDesc = "Bạn không có quyền sử dụng chức năng này";
                    return Json(ReturnData);
                }

                if (groupId > 0)
                {
                    var result = AbstractDAOFactory.Instance().CreateFunctionDAO().DeleteGroup(groupId);
                    ReturnData.ResponseCode = result;
                    if (result >= 0)
                    {
                        ReturnData.ResponseDesc = "Xóa nhóm Thành Công";
                    }
                    else switch (result)
                        {
                            case -51: ReturnData.ResponseDesc = "Nhóm tài khoản không tồn tại trong hệ thống"; break;
                            case -99: ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                            default: ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau"; break;
                        }
                    return Json(ReturnData);
                }
                ReturnData.ResponseCode = -100;
                ReturnData.ResponseDesc = "Không xác định được nhóm cần xóa";
                return Json(ReturnData);
            }
            catch (Exception ex)
            {
                NLogLogger.PublishException(ex);
                ReturnData.ResponseCode = -99;
                ReturnData.ResponseDesc = "Hệ thống đang bận. Vui lòng quay lại sau";
                return Json(ReturnData);
            }
        }
        #endregion

        #region PRIVATE
        public bool CheckPermissionUser(string Action, int UserId, bool IsAdmin = false)
        {
            try
            {
                if (string.IsNullOrEmpty(Action))
                {
                    return false;
                }
                var m_function = AbstractDAOFactory.Instance().CreateFunctionDAO().
                    GetFunctionByActionName(Action);
                if (m_function == null || m_function.FunctionID <= 0)
                    return false;

                var permission = new UserFunction();
                //Nếu user là admin -> full quyền
                if (IsAdmin)
                {
                    permission.FunctionID = m_function.FunctionID;
                    permission.FunctionName = m_function.FunctionName;
                    permission.ActionName = Action;
                    permission.UserID = UserId;
                    permission.IsGrant = true;
                    permission.IsInsert = true;
                    permission.IsUpdate = true;
                    permission.IsDelete = true;
                }
                else //Lấy quyền của user khi truy cập chức năng
                {
                    permission = AbstractDAOFactory.Instance().CreateUserRoleDAO().
                        CheckPermission(UserId, Action);
                }

                if (permission == null || permission.FunctionID == 0)
                {
                    //HttpContext.Current.Session.Abandon();
                    return false;
                }
                HttpContext.Session.SetObject(SessionState.SESSION_PERMISSION, permission);
                return true;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return false;
            }
        }
        #endregion
    }
}

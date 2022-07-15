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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace ODIN_CMS.Controllers
{
    public class TeacherController : Controller
    {
        private static int UserID = 0;
        private static int GroupID = 0;
        private static int MerchantID = 1;

        private readonly ILogger<TeacherController> _logger;

        private IHostingEnvironment _environment;
        public TeacherController(ILogger<TeacherController> logger, IHostingEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TeacherManager()
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
            ViewBag.GroupID = GroupID;
            return View();
        }

        public IActionResult TableListTeacher(string keyword, int status, int merchantId)
        {
            var data = new List<Teacher>();
            UserID = IdentityHelper.UserID;
            GroupID = IdentityHelper.GroupID;
            MerchantID = IdentityHelper.MerchantID;

            int IdMerchant = merchantId == -1 ? MerchantID : (int)merchantId;

            data = AbstractDAOFactory.Instance().CreateCMSDAO().TeacherGetList(keyword, status, IdMerchant);

            return PartialView("TableListTeacher", data);
        }

        public JsonResult TeacherGetList(string keyword, int status, int merchantId)
        {
            var lstTeacher = new List<Teacher>();
            lstTeacher = AbstractDAOFactory.Instance().CreateCMSDAO().TeacherGetList(keyword, status, merchantId);

            return Json(new { Response = lstTeacher.Count, message = "OK", Data = lstTeacher });

        }

        public IActionResult GetTeacherInfo(int? id)
        {
            UserID = IdentityHelper.UserID;
            GroupID = IdentityHelper.GroupID;
            MerchantID = IdentityHelper.MerchantID;
            ViewBag.MerchantID = 0;
            ViewBag.MerchantName = string.Empty;
            int TeacherId = id == null ? 0 : (int)id;
            var teacherInfo = new Teacher();
            var lstMerchant = new List<Merchant>();
            var model = new TeacherInfoViewModel();

            if (TeacherId > 0)
            {
                teacherInfo = AbstractDAOFactory.Instance().CreateCMSDAO().GetTeacherInfoByID(TeacherId);
                if (!string.IsNullOrEmpty(teacherInfo.Cmnd_Path))
                {
                    var strCmnd = teacherInfo.Cmnd_Path.Split(";");
                    if (strCmnd.Length > 1)
                    {
                        teacherInfo.Cmnd_After = strCmnd[0];
                        teacherInfo.Cmnd_Before = strCmnd[1];
                    }
                    else if (strCmnd.Length > 0 && strCmnd.Length < 2)
                    {
                        teacherInfo.Cmnd_After = strCmnd[0];
                    }
                }
                teacherInfo.CmndNum = teacherInfo.CmndNum.Trim();
                ViewBag.MerchantID = teacherInfo.MerchantID;
                ViewBag.MerchantName = teacherInfo.MerchantName;
            }

            lstMerchant = AbstractDAOFactory.Instance().CreateCMSDAO().MerchantGetList(MerchantID);
            model.Teacher = teacherInfo;
            model.ListMerchant = lstMerchant;
            return PartialView("GetTeacherInfo", model);
        }

        public IActionResult TeacherDetails(int id)
        {
            return View();
        }

        public JsonResult SaveDataTeacher(Teacher teacher)
        {
            var ReturnData = new ReturnData();
            UserID = IdentityHelper.UserID;
            GroupID = IdentityHelper.GroupID;
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    var url = Config.URL_ROOT + "Account/FormLogin";
                    var urlLogout = string.Format("{0}?act=out", url);
                    Response.Redirect(urlLogout, true);
                }
                var groupFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionDetail("TeacherManager", GroupID);

                if (groupFunction == null || (teacher.TeacherID == 0 && !groupFunction.IsInsert) || teacher.TeacherID > 0 && !groupFunction.IsUpdate)
                {
                    ReturnData.ResponseCode = -101;
                    ReturnData.ResponseDesc = "Bạn không có quyền sử dụng chức năng này";
                    return Json(ReturnData);
                }
                if (string.IsNullOrEmpty(teacher.FullName))
                {
                    ReturnData.ResponseCode = -6001;
                    ReturnData.ResponseDesc = "Bạn chưa nhập họ tên giáo viên";
                    return Json(ReturnData);
                }
                if (string.IsNullOrEmpty(teacher.Email) || !teacher.Email.Contains("@"))
                {
                    ReturnData.ResponseCode = -6002;
                    ReturnData.ResponseDesc = "Email chưa nhập hoặc sai định dạng";
                    return Json(ReturnData);
                }

                teacher.MerchantID = teacher.MerchantID == null ? IdentityHelper.MerchantID : teacher.MerchantID;
                teacher.CmndNum = teacher.CmndNum.Trim();
                teacher.PhoneNumber = teacher.PhoneNumber.Trim();
                var result = AbstractDAOFactory.Instance().CreateCMSDAO().INUP_Teacher_CMS(teacher);
                ReturnData.ResponseCode = result;
                if (result >= 0)
                {
                    ReturnData.ResponseDesc = teacher.TeacherID > 0 ? "Cập nhật Thành Công" : "Thêm mới Thành Công";
                }
                else switch (result)
                    {
                        case -51: ReturnData.ResponseDesc = "Giáo viên đã tồn tại"; break;
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
        public JsonResult UpdateActive(int id, int status)
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
                var groupFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionDetail("TeacherManager", GroupID);

                if (groupFunction == null || !groupFunction.IsInsert || !groupFunction.IsUpdate)
                {
                    ReturnData.ResponseCode = -101;
                    ReturnData.ResponseDesc = "Bạn không có quyền sử dụng chức năng này";
                    return Json(ReturnData);
                }
                if (id >= 0)
                {
                    var m_user = AbstractDAOFactory.Instance().CreateCMSDAO().GetTeacherInfoByID(id);
                    if (m_user == null || m_user.TeacherID <= 0)
                    {
                        ReturnData.ResponseCode = -101;
                        ReturnData.ResponseDesc = "Không tồn tại tài khoản";
                        return Json(ReturnData);
                    }
                    var result = AbstractDAOFactory.Instance().CreateCMSDAO().UpdateTeacherStatus(id, status);
                    if (result >= 0)
                    {
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
                ReturnData.ResponseDesc = "Không xác định giáo viên cần active";
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
        public JsonResult DeleteTeacher(int id)
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
                var groupFunction = AbstractDAOFactory.Instance().CreateFunctionDAO().GroupFunctionDetail("TeacherManager", GroupID);

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
                    var m_user = AbstractDAOFactory.Instance().CreateCMSDAO().GetTeacherInfoByID(id);
                    if (m_user == null || m_user.TeacherID <= 0)
                    {
                        ReturnData.ResponseCode = -101;
                        ReturnData.ResponseDesc = "Không tồn tại giáo viên cần xóa";
                        return Json(ReturnData);
                    }
                    var result = AbstractDAOFactory.Instance().CreateCMSDAO().Delete_Teacher(id);
                    if (result >= 0)
                    {
                        ReturnData.ResponseDesc = "Xóa giáo viên Thành Công";
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
                ReturnData.ResponseDesc = "Không xác định giáo viên cần xóa";
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
        [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
        [RequestSizeLimit(52428800)]
        public JsonResult UploadFileCmnd()
        {
            var files = HttpContext.Request.Form.Files;
            var fileName = "";
            var imageUrl = "";
            //Lưu ảnh lên thư mục trên server
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var file = Image;
                    //There is an error here
                    //var uploads = Path.Combine(_appEnvironment.WebRootPath, "media/images/");
                    var uploads = Path.Combine(Config.MEDIA_DISK + "Teacher/");
                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);
                    NLogLogger.LogInfo("UPLOAD IMAGE ARTICLE PATH :" + uploads);
                    try
                    {
                        if (file.Length > 0)
                        {
                            fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                            }
                            imageUrl = Config.IMAGE_URL + "Teacher/" + fileName;
                        }
                    }
                    catch (Exception ex)
                    {
                        NLogLogger.LogInfo("UploadFile Exception : " + ex.Message);
                    }
                }
            }
            return Json(new { FileName = imageUrl, ResCode = 1 });
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 52428800)]
        [RequestSizeLimit(52428800)]
        public JsonResult UploadFileAvatar()
        {
            var files = HttpContext.Request.Form.Files;
            var fileName = "";
            var imageUrl = "";
            //Lưu ảnh lên thư mục trên server
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var file = Image;
                    //There is an error here
                    var uploads = Path.Combine(Config.MEDIA_DISK + "Teacher/Avatar/");
                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);
                    NLogLogger.LogInfo("UPLOAD IMAGE ARTICLE PATH :" + uploads);
                    try
                    {
                        if (file.Length > 0)
                        {
                            fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                            }
                            imageUrl = Config.IMAGE_URL + "Teacher/Avatar/" + fileName;
                        }
                    }
                    catch (Exception ex)
                    {
                        NLogLogger.LogInfo("UploadFile Exception : " + ex.Message);
                    }
                }
            }
            return Json(new { FileName = imageUrl, ResCode = 1 });
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}

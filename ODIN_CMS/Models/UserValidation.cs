using System;
using Utils;
using DataAccess.Factory;
using DataAccess.Models;
using ODIN_CMS.Controllers.Common;
using Microsoft.AspNetCore.Http;

namespace ODIN_CMS.Models
{
    public class UserValidation
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public UserValidation(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public  bool CheckPermissionUser(string Action, int UserId, bool IsAdmin = false)
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
                _session.SetObject(SessionState.SESSION_PERMISSION, permission);
               // HttpContext.Session.SetObject(SessionState.SESSION_PERMISSION, permission);
                return true;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return false;
            }
        }
    }
}

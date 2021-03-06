using System;
using DataAccess.Interface;
using DataAccess.Models;
using System.Data;
using Utils;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace DataAccess.Implement
{
    public class UserRoleDAOImpl : IUserRoleDAO
    {
        #region Quyền truy cập chức năng

        /// Check quyền người dùng khi truy cập chức năng
        /// <param name="UserID"></param>
        /// <param name="FunctionID"></param>
        /// <returns></returns>
        public UserFunction CheckPermission(int UserID, string ActionName)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_UserID", UserID);
                pars[1] = new SqlParameter("@_ActionName", ActionName);
                var result = new DBHelper(Config.ConnectionString.SQLConnCMS).GetInstanceSP<UserFunction>("SP_CheckPermission", pars);
                return result;
            }
            catch (Exception e)
            {
                NLogLogger.PublishException(e);
                return null;
            }
        }

        /// Thêm quyền người dùng
        /// <param name="RoleFunction"></param>
        /// <returns></returns>
        public int UserFunctionInsert(UserFunction RoleFunction)
        {
            try
            {
                var pars = new SqlParameter[8];
                pars[0] = new SqlParameter("@_UserID", RoleFunction.UserID);
                pars[1] = new SqlParameter("@_FunctionID", RoleFunction.FunctionID);
                pars[2] = new SqlParameter("@_IsGrant", RoleFunction.IsGrant);
                pars[3] = new SqlParameter("@_IsInsert", RoleFunction.IsInsert);
                pars[4] = new SqlParameter("@_IsUpdate", RoleFunction.IsUpdate);
                pars[5] = new SqlParameter("@_IsDelete", RoleFunction.IsDelete);
                pars[6] = new SqlParameter("@_CreatedUserID", RoleFunction.CreatedUserID);
                pars[7] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_UserFunction_InsertOne", pars);
                return Convert.ToInt32(pars[7].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        /// Cấp quyền cho user theo danh sách quyền
        /// <param name="UserID">Mã user</param>
        /// <param name="ListRole">danh sách quyền có dạng (FunctionID, IsGrant, IsInsert,IsUpdate,IsDelete) phân cách bằng ký tự ";"</param>
        /// <param name="CreateUserID">Người tạo</param>
        /// <param name="IsAdmin">
        ///     True : Xóa toàn bộ chức năng quyền của User -> cấp lại theo danh sách quyền truyền vào
        ///     False (trường hợp là Mod) : Xóa danh sách quyền theo danh sách quyền truyền vào -> cấp lại theo danh sách quyền truyền vào
        /// </param>
        /// <returns></returns>
        public int UserFunctionInsertList(int UserID, string ListRole, int CreateUserID, bool IsAdmin)
        {
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_UserID", UserID);
                pars[1] = new SqlParameter("@_PermissionOfFnIDs", ListRole);
                pars[2] = new SqlParameter("@_CreatedUserID", CreateUserID);
                pars[3] = new SqlParameter("@_IsAdmin", IsAdmin);
                pars[4] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_UserFunction_Insert", pars);
                return Convert.ToInt32(pars[4].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }
        /// <summary>
        /// phân quyền chức năng cho danh sách user
        /// </summary>
        /// <param name="FunctionID"></param>
        /// <param name="ListRole"></param>
        /// <param name="CreateUserID"></param>
        /// <returns></returns>
        public int UserFunctionInsertListByFunctionID(int FunctionID, string ListRole, int CreateUserID)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_FunctionID", FunctionID);
                pars[1] = new SqlParameter("@_PermissionOfUsers", ListRole);
                pars[2] = new SqlParameter("@_CreatedUserID", CreateUserID);
                pars[3] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_UserFunction_InsertList_byFunctionID", pars);
                return Convert.ToInt32(pars[3].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }
        /// Xóa quyền theo chức năng và user
        /// <param name="UserID"></param>
        /// <param name="FunctionID"></param>
        /// <returns></returns>
        public int UserFunctionDelete(int UserID, int FunctionID)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_UserID", UserID);
                pars[1] = new SqlParameter("@_FunctionID", FunctionID);
                pars[2] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_UserFunction_Delete", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        /// Xóa danh sách quyền bởi UserID và danh sách quyền đc chọn
        /// <param name="UserID"></param>
        /// <param name="ListFunction"></param>
        /// <returns></returns>
        public int UserFunctionDeleteList(int UserID, string ListFunction)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_UserID", UserID);
                pars[1] = new SqlParameter("@_FunctionID", ListFunction);
                pars[2] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_UserFunction_DeleteList", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        /// Xóa toàn bộ danh sách quyền của người dùng
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int UserFunctionDeleteAll(int UserID)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_UserID", UserID);
                pars[1] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_UserFunction_DeleteAllByUserID", pars);
                return Convert.ToInt32(pars[1].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        /// Lấy danh sách quyền dịch vụ của người dùng
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<UserFunction> UserFunction_GetByUserID(int UserID)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_UserID", UserID);
                var list = new DBHelper(Config.ConnectionString.SQLConnCMS).GetListSP<UserFunction>("SP_UserFunction_GetByUserID", pars);
                if (list == null || list.Count <= 0)
                    return new List<UserFunction>();
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new List<UserFunction>();
            }
        }

        public List<UserFunction> GetListUser_ByRoleFunctionID(int FunctionID, int IsGrant)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_FunctionID", FunctionID);
                pars[1] = new SqlParameter("@_IsGrant", IsGrant);
                var list = new DBHelper(Config.ConnectionString.SQLConnCMS).GetListSP<UserFunction>("SP_User_GetList_ByRoleFunctionID", pars);
                if (list == null || list.Count <= 0)
                    return new List<UserFunction>();
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new List<UserFunction>();
            }
        }

        public int GroupFunctionInsertList(int GroupID, string ListRole, int Type)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_GroupID", GroupID);
                pars[1] = new SqlParameter("@_PermissionOfFnIDs", ListRole);
                pars[2] = new SqlParameter("@_Type", Type);
                pars[3] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_GroupFunction_Insert", pars);
                return Convert.ToInt32(pars[3].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        public int GroupFunctionInsertListByFunctionID(int FunctionID, string ListRole, int CreateGroupID)
        {
            throw new NotImplementedException();
        }

        public int GroupFunctionDelete(int GroupID, int FunctionID)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_GroupID", GroupID);
                pars[1] = new SqlParameter("@_FunctionID", FunctionID);
                pars[2] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_GroupFunction_Delete", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        public int GroupFunctionDeleteList(int GroupID, string ListFunction)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_GroupID", GroupID);
                pars[1] = new SqlParameter("@_FunctionID", ListFunction);
                pars[2] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_GroupFunction_DeleteList", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        public int GroupFunctionDeleteAll(int GroupID)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_GroupID", GroupID);
                pars[1] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_GroupFunction_DeleteAllByGroupID", pars);
                return Convert.ToInt32(pars[1].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        public List<GroupFunctions> GroupFunction_GetByGroupID(int GroupID)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_GroupID", GroupID);
                var list = new DBHelper(Config.ConnectionString.SQLConnCMS).GetListSP<GroupFunctions>("SP_GroupFunction_GetByGroupID", pars);
                if (list == null || list.Count <= 0)
                    return new List<GroupFunctions>();
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new List<GroupFunctions>();
            }
        }

        public List<GroupFunctions> GetListGroup_ByRoleFunctionID(int FunctionID, int IsGrant)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}

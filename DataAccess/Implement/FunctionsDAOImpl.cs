using System;
using DataAccess.Interface;
using DataAccess.Models;
using System.Data;
using Utils;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DataAccess.Implement
{
    public class FunctionsDAOImpl : IFucntionsDAO
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public FunctionsDAOImpl(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public FunctionsDAOImpl()
        {
        }

        /// <summary>
        /// Lấy FunctionID theo tên funtion
        /// </summary>
        /// <param name="functionname"></param>
        /// <returns></returns>
        public int GetFunctionID(string functionname)
        {
            try
            {
                var functionlist = _session.GetString(SessionState.SESSION_FUNCTIONS);
                var func = JsonConvert.DeserializeObject<List<Functions>>(functionlist);
                var fun = func.Find(f => f.Url.ToLower().LastIndexOf("/" + functionname.ToLower()) > 0);
                return fun != null ? fun.FunctionID : 0;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }


        /// <summary>
        /// Lấy một Function theo FunctionID
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public Functions GetFunctionByFunctionID(int functionId)
        {
            try
            {
                return new DBHelper(Config.ConnectionString.SQLConnCMS).GetInstanceSP<Functions>("SP_Functions_GetByFunctionID",
                                                                                                   new SqlParameter("@_FunctionID", functionId));
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new Functions();
            }
        }
        /// <summary>
        /// Lấy một Function theo FunctionID
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public Functions GetFunctionByActionName(string ActionName)
        {
            try
            {
                return new DBHelper(Config.ConnectionString.SQLConnCMS).GetInstanceSP<Functions>("SP_Functions_GetByActionName",
                                                                                                   new SqlParameter("@_ActionName", ActionName));
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new Functions();
            }
        }


        /// <summary>
        /// Lấy danh sách funtion theo UserID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Functions> GetListFunctionByUserID(int userId, int grant)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_UserID", userId);
                pars[1] = new SqlParameter("@_GetGrant", grant < 0 ? DBNull.Value : (object)grant);
                return new DBHelper(Config.ConnectionString.SQLConnCMS).GetListSP<Functions>("SP_Function_GetByUserID", pars);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new List<Functions>();
            }
        }

        /// <summary>
        /// Lây danh sách Fucntion theo các tham số truyền vào, Có phân trang
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="isAcitve"></param>
        /// <param name="systemId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Functions> GetListFunctions(string Keyword, int isAcitve, int pageNumber, int pageSize, ref int TotalRecord)
        {
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_Keyword", string.IsNullOrEmpty(Keyword) ? string.Empty : Keyword);
                pars[1] = new SqlParameter("@_Status", isAcitve);
                pars[2] = new SqlParameter("@_CurrPage", pageNumber);
                pars[3] = new SqlParameter("@_RecordPerPage", pageSize);
                pars[4] = new SqlParameter("@_TotalRecord", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var list = new DBHelper(Config.ConnectionString.SQLConnCMS).GetListSP<Functions>("SP_Functions_GetPage", pars);
                TotalRecord = Convert.ToInt32(pars[4].Value);
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                TotalRecord = 0;
                return new List<Functions>();
            }
        }

        public List<Functions> GetListFunctionsByFather(int FatherID)
        {
            try
            {
                var list = new DBHelper(Config.ConnectionString.SQLConnCMS).GetListSP<Functions>("SP_Functions_GetParent",
                    new SqlParameter("@_ParentID", FatherID));
                if (list == null || list.Count <= 0)
                    return new List<Functions>();
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new List<Functions>();
            }
        }

        /// <summary>
        /// Insert Fucntion
        /// </summary>
        /// <param name="functions"></param>
        /// <returns> >0 : thanh cong
        ///			-1: da ton tai
        ///			-99: loi he thong
        /// </returns>
        public int InsertUpdateFunction(Functions functions)
        {
            try
            {
                var pars = new SqlParameter[10];
                pars[0] = new SqlParameter("@_FunctionID", functions.FunctionID);
                pars[1] = new SqlParameter("@_FunctionName", functions.FunctionName);
                pars[2] = new SqlParameter("@_Url", functions.Url);
                pars[3] = new SqlParameter("@_IsDisplay", functions.IsDisplay);
                pars[4] = new SqlParameter("@_IsActive", functions.IsActive);
                pars[5] = new SqlParameter("@_ParentID", functions.ParentID);
                pars[6] = new SqlParameter("@_Order", functions.Order);
                pars[7] = new SqlParameter("@_CssIcon", functions.CssIcon);
                pars[8] = new SqlParameter("@_ActionName", functions.ActionName);
                pars[9] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_Functions_Update", pars);
                return Convert.ToInt32(pars[9].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }
        public int UpdateOrder(int FunctionID, int ParentID, int Order)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_FunctionID", FunctionID);
                pars[1] = new SqlParameter("@_ParentID", ParentID);
                pars[2] = new SqlParameter("@_Order", Order);
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_Functions_UpdateOrder", pars);
                return Convert.ToInt32(pars[9].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        /// <summary>
        /// Xóa Functions
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public int DelleteFunction(int functionId)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_FunctionID", functionId);
                pars[1] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_Functions_Delete", pars);
                return Convert.ToInt32(pars[1].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        public List<Functions> SelectAllFunctionID(int fatherID, string name, int isactive, int isdisplay)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_ParentID", fatherID);
                pars[1] = new SqlParameter("@_FunctionName", name);
                pars[2] = new SqlParameter("@_IsActive", isactive);
                pars[3] = new SqlParameter("@_IsDisplay", isdisplay);
                var result = new DBHelper(Config.ConnectionString.SQLConnCMS).GetListSP<Functions>("SP_Functions_SelectByCondition", pars);
                return result;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new List<Functions>();
            }

        }

        #region GROUP FUNCTION
        public List<Groups> GetListGroups(int groupId)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_GroupID", groupId);
                var result = new DBHelper(Config.ConnectionString.SQLConnCMS).GetListSP<Groups>("SP_Group_GetAll", pars);
                return result;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new List<Groups>();
            }
        }

        public List<GroupFunctions> GroupFunctionGetList(int groupId, int type)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_GroupID", groupId);
                pars[1] = new SqlParameter("@_Type", type);
                var result = new DBHelper(Config.ConnectionString.SQLConnCMS).GetListSP<GroupFunctions>("SP_Group_Function_GetList", pars);
                return result;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new List<GroupFunctions>();
            }
        }

        public int GroupEdit(GroupEdit groups)
        {
            try
            {
                var pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@_GroupID", groups.GroupID);
                pars[1] = new SqlParameter("@_ExeUserID", groups.ExeUserID);
                pars[2] = new SqlParameter("@_CreatedBy", groups.CreatedBy);
                pars[3] = new SqlParameter("@_GroupName", groups.Name);
                pars[4] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_Group_Edit", pars);
                return Convert.ToInt32(pars[4].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        public int GroupFunctionEdit(GroupFunctionEdit functions)
        {
            try
            {
                var pars = new SqlParameter[9];
                pars[0] = new SqlParameter("@_ExeType", functions.ExeType);
                pars[1] = new SqlParameter("@_ExeUserID", functions.ExeUserID);
                pars[2] = new SqlParameter("@_GroupID", functions.GroupID);
                pars[3] = new SqlParameter("@_FunctionID", functions.FunctionID);
                pars[4] = new SqlParameter("@_IsView", functions.IsView);
                pars[5] = new SqlParameter("@_IsInsert", functions.IsInsert);
                pars[6] = new SqlParameter("@_IsUpdate", functions.IsUpdate);
                pars[7] = new SqlParameter("@_IsDelete", functions.IsDelete);
                pars[8] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_Group_Edit", pars);
                return Convert.ToInt32(pars[8].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        public GroupFunctionDetail GroupFunctionDetail(string action, int groupId)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_ActionName", action);
                pars[1] = new SqlParameter("@_GroupID", groupId);
                return new DBHelper(Config.ConnectionString.SQLConnCMS).GetInstanceSP<GroupFunctionDetail>("SP_GroupFunction_GetDetai_ByAction", pars);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new GroupFunctionDetail();
            }
        }

        public int DeleteGroup(int groupId)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_GroupID", groupId);
                pars[1] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_Group_Delete", pars);
                return Convert.ToInt32(pars[1].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        public GroupEdit GetInfo(int groupId)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_GroupID", groupId);
                return new DBHelper(Config.ConnectionString.SQLConnCMS).GetInstanceSP<GroupEdit>("SP_Group_GetInfo", pars);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new GroupEdit();
            }
        }

        #endregion
    }
}


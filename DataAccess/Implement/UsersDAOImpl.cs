using System;
using DataAccess.Interface;
using DataAccess.Models;
using System.Data;
using Utils;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace DataAccess.Implement
{
    public class UsersDAOImpl : IUsersDAO
    {

        /// <summary>
        /// Xác thực người dùng
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="isSucess"></param>
        /// <returns></returns>
        public int Authentication(string username, string password)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_Username", username);
                pars[1] = new SqlParameter("@_Password", password);
                pars[2] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_User_Authenticate", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                NLogLogger.PublishException(ex);
                return -99;
            }
        }


        /// <summary>
        /// Get User theo UserID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Users SelectByUserID(int userId)
        {
            try
            {

                return new DBHelper(Config.ConnectionString.SQLConnCMS).GetInstanceSP<Users>("SP_User_GetUserInfo_ByID",
                                                                                                 new SqlParameter("@ID", userId));

            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new Users();
            }
        }



        /// <summary>
        /// Get User theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Users GetByEmail(string email)
        {
            try
            {
                return new DBHelper(Config.ConnectionString.SQLConnCMS).GetInstanceSP<Users>("SP_User_GetByCondition",
                                                                                                 new SqlParameter("@_Email", email));
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new Users();
            }
        }
        /// <summary>
        /// Get User theo Username
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public Users GetByUsername(string Username)
        {
            try
            {
                return new DBHelper(Config.ConnectionString.SQLConnCMS).GetInstanceSP<Users>("SP_User_GetInfo_ByName",
                                                                                                 new SqlParameter("@_Username", Username));
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new Users();
            }
        }



        /// <summary>
        /// Get list<Users>theo điều kiện, có phân trang
        /// </summary>
        /// <param name="departmentID"></param>
        /// <param name="groupID"></param>
        /// <param name="isAcitve"></param>
        /// <param name="email"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Users> GetListUsers(string Keyword, int isActive, int groupId, int merchantId)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_Status", isActive);
                pars[1] = new SqlParameter("@_Keyword", Keyword);
                pars[2] = new SqlParameter("@_GroupId", groupId == -1 ? 1 : (object)groupId);
                pars[3] = new SqlParameter("@_MerchantId", merchantId == -1 ? 1 : (object)merchantId);
                var list = new DBHelper(Config.ConnectionString.SQLConnCMS).GetListSP<Users>("SP_User_GetPage", pars);
                if (list == null || list.Count <= 0)
                    return new List<Users>();
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new List<Users>();
            }
        }
        /// <summary>
        /// Lấy tất cả user theo trạng thái
        /// </summary>
        /// <param name="Status"> -1 : tất cả, 0 : unactive, 1:active</param>
        /// <returns></returns>
        public List<Users> GetAllUserByStatus(int Status)
        {
            try
            {
                var list = new DBHelper(Config.ConnectionString.SQLConnCMS)
                    .GetListSP<Users>("SP_User_GetAll_ByStatus", new SqlParameter("@_Status", Status));
                if (list == null || list.Count <= 0)
                    return new List<Users>();
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new List<Users>();
            }
        }

        /// <summary>
        /// Update thông tin User
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public int UpdateUsers(Users users)
        {
            try
            {
                var pars = new SqlParameter[11];
                pars[0] = new SqlParameter("@_UserID", (users.UserID < 0) ? 0 : users.UserID);
                pars[1] = new SqlParameter("@_Username", string.IsNullOrEmpty(users.Username) ? DBNull.Value : (object)users.Username);
                pars[2] = new SqlParameter("@_Email", string.IsNullOrEmpty(users.Email) ? DBNull.Value : (object)users.Email);
                pars[3] = new SqlParameter("@_FullName", string.IsNullOrEmpty(users.FullName) ? DBNull.Value : (object)users.FullName);
                pars[4] = new SqlParameter("@_Password", string.IsNullOrEmpty(users.Password) ? DBNull.Value : (object)users.Password);
                pars[5] = new SqlParameter("@_IsActive", users.Status);
                pars[6] = new SqlParameter("@_GroupID", users.GroupID);
                pars[7] = new SqlParameter("@_PhoneNumber", string.IsNullOrEmpty(users.PhoneNumber) ? DBNull.Value : (object)users.PhoneNumber);
                pars[8] = new SqlParameter("@_CreatedUser", string.IsNullOrEmpty(users.CreatedUser) ? DBNull.Value : (object)users.CreatedUser);
                pars[9] = new SqlParameter("@_MerchantID", users.MerchantID);
                pars[10] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_User_InsertUpdate", pars);
                return Convert.ToInt32(pars[10].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }



        /// <summary>
        /// Xóa thông tin một user theo UserID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int DelleteUsers(int userId)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_UserID", userId);
                pars[1] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_User_Delete", pars);
                return Convert.ToInt32(pars[1].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }


        public int UpdateActiveUser(int Id)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_UserID", Id);
                pars[1] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_User_UpdateActive", pars);
                return Convert.ToInt32(pars[1].Value);
            }
            catch (Exception e)
            {
                NLogLogger.PublishException(e);
                return -99;
            }
        }

        public int ChangePassword(string UserName, string PasswordOld, string PasswordNew)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@_UserName", UserName);
                pars[1] = new SqlParameter("@_PasswordOld", PasswordOld);
                pars[2] = new SqlParameter("@_PasswordNew", PasswordNew);
                pars[3] = new SqlParameter("@_ResponseCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_User_ChangePassword", pars);
                return Convert.ToInt32(pars[3].Value);
            }
            catch (Exception e)
            {
                NLogLogger.PublishException(e);
                return -99;
            }
        }

        public Users GetAccountInfo(int userId)
        {
            try
            {


                return new DBHelper(Config.ConnectionString.SQLConnCMS).GetInstanceSP<Users>("SP_User_GetUserInfo_ByID",
                                                                                                 new SqlParameter("@ID", userId));
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new Users();
            }
        }

        public int ChangeAvatar(int userId, string avatar)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@_UserID", userId);
                pars[1] = new SqlParameter("@_Avatar", avatar);
                pars[2] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_User_ChangeAvatar", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception e)
            {
                NLogLogger.PublishException(e);
                return -99;
            }
        }

        public Users GetUserInfo(string username)
        {
            try
            {
                return new DBHelper(Config.ConnectionString.SQLConnCMS).GetInstanceSP<Users>("SP_User_GetInfo_ByName",
                                                                                                 new SqlParameter("@_Username", username));
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new Users();
            }
        }

        public int GetAccountIdByTeleChatId(long chatId)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@ChatId", chatId);
                pars[1] = new SqlParameter("@UserId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_GetUserIDByTeleChatID", pars);
                return Convert.ToInt32(pars[1].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        public void InsertLogSMSOTP(int userId, long chatId, string Otp, bool IsLogin)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@UserId", userId);
                pars[1] = new SqlParameter("@ChatId", chatId);
                pars[2] = new SqlParameter("@Otp", Otp);
                pars[3] = new SqlParameter("@IsLogin", IsLogin);
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_InsertLogSMSOTP", pars);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
            }
        }

        public int AuthenOTP(int userId, string Otp)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@UserId", userId);
                pars[1] = new SqlParameter("@Otp", Otp);
                pars[2] = new SqlParameter("@ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_User_AuthenOTP", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception e)
            {
                NLogLogger.PublishException(e);
                return -99;
            }
        }

        public int GetAccountByPhone(string phone)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@PhoneNumber", phone);
                pars[1] = new SqlParameter("@UserId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_CheckUserIdByPhone", pars);
                return Convert.ToInt32(pars[1].Value);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return -99;
            }
        }

        public void UpdateChatIdTeleBot(int userId, long chatId)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@UserId", userId);
                pars[1] = new SqlParameter("@ChatId", chatId);
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_UpdateChatIdTeleBot", pars);
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
            }
        }
    }
}

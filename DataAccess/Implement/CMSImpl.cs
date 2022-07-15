using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using DataAccess.Interface;
using DataAccess.Models;
using Utils;
namespace DataAccess.Implement
{
    public class CMSImpl : ICMS
    {
        public List<Languages> GetListLanguage_CMS(int langType, int status, ref int totalRow)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@LanguageType", langType);
                pars[1] = new SqlParameter("@Status", status);
                pars[2] = new SqlParameter("@TotalRow", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var list = new DBHelper(Config.ConnectionString.SQLConnWEB).GetListSP<Languages>("SP_LanguageType_GetList_CMS", pars);
                if (list == null || list.Count <= 0)
                    return new List<Languages>();
                totalRow = Convert.ToInt32(pars[2].Value);
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                totalRow = 0;
                return new List<Languages>();
            }
        }

        public int INUP_Language_CMS(Languages language)
        {
            try
            {
                var pars = new SqlParameter[7];
                pars[0] = new SqlParameter("@LanguageType", language.LanguageType);
                pars[1] = new SqlParameter("@Language", language.Language);
                pars[2] = new SqlParameter("@Code", language.Code);
                pars[3] = new SqlParameter("@Icon", language.Icon);
                pars[4] = new SqlParameter("@Status", language.Status);
                pars[5] = new SqlParameter("@CreateUser", language.CreateUser);
                pars[6] = new SqlParameter("@ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnWEB).ExecuteNonQuerySP("SP_LanguageType_INUP_CMS", pars);
                return Convert.ToInt32(pars[6].Value);
            }
            catch (Exception e)
            {
                NLogLogger.PublishException(e);
                return -99;
            }
        }

        public List<Merchant> MerchantGetList(int merchantId)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@MerchantID", merchantId);
                var list = new DBHelper(Config.ConnectionString.SQLConnCMS).GetListSP<Merchant>("SP_Merchant_GetList", pars);
                if (list == null || list.Count <= 0)
                    return new List<Merchant>();
               
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new List<Merchant>();
            }
        }


        #region TEACHER
        public List<Teacher> TeacherGetList(string keyword, int status, int merchantId)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@Keyword", keyword);
                pars[1] = new SqlParameter("@Status", status);
                pars[2] = new SqlParameter("@MerchantID", merchantId);
                var list = new DBHelper(Config.ConnectionString.SQLConnCMS).GetListSP<Teacher>("SP_Teacher_GetList", pars);
                if (list == null || list.Count <= 0)
                    return new List<Teacher>();

                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new List<Teacher>();
            }
        }

        public Teacher GetTeacherInfoByID(int teacherId)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@TeacherID", teacherId);
                var list = new DBHelper(Config.ConnectionString.SQLConnCMS).GetInstanceSP<Teacher>("SP_Teacher_GetInfoByID", pars);
                if (list == null)
                    return new Teacher();
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new Teacher();
            }
        }

        public int Delete_Teacher(int teacherId)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@TeacherID", teacherId);
                pars[1] = new SqlParameter("@ResponeStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_Teacher_Delete", pars);
                return Convert.ToInt32(pars[1].Value);
            }
            catch (Exception e)
            {
                NLogLogger.PublishException(e);
                return -99;
            }
        }
        public int INUP_Teacher_CMS(Teacher teacher)
        {
            try
            {
                var pars = new SqlParameter[13];
                pars[0] = new SqlParameter("@TeacherID", teacher.TeacherID);
                pars[1] = new SqlParameter("@FullName", teacher.FullName);
                pars[2] = new SqlParameter("@Email", teacher.Email);
                pars[3] = new SqlParameter("@PhoneNumber", teacher.PhoneNumber);
                pars[4] = new SqlParameter("@CmndNum", teacher.CmndNum);
                pars[5] = new SqlParameter("@DateBy", teacher.DateBy);
                pars[6] = new SqlParameter("@IssuedBy", teacher.IssuedBy);
                pars[7] = new SqlParameter("@Cmnd_Path", teacher.Cmnd_Path);
                pars[8] = new SqlParameter("@Certificate", teacher.Certificate);
                pars[9] = new SqlParameter("@MerchantID", teacher.MerchantID);
                pars[10] = new SqlParameter("@Status", teacher.Status);
                pars[11] = new SqlParameter("@ImagePath", teacher.ImagePath);
                pars[12] = new SqlParameter("@ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_Teacher_InsertUpdate", pars);
                return Convert.ToInt32(pars[12].Value);
            }
            catch (Exception e)
            {
                NLogLogger.PublishException(e);
                return -99;
            }
        }
        public int UpdateTeacherStatus(int teacherId, int status)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@TeacherID", teacherId);
                pars[1] = new SqlParameter("@Status", status);
                pars[2] = new SqlParameter("@ResponeStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_Teacher_UpdateStatus", pars);
                return Convert.ToInt32(pars[2].Value);
            }
            catch (Exception e)
            {
                NLogLogger.PublishException(e);
                return -99;
            }
        }
        #endregion

        #region FINANCE
        public List<Finance> FinanceGetList(string keyword, string startTime, string endTime, int merchantId)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@Keyword", keyword);
                pars[1] = new SqlParameter("@StartTime", startTime);
                pars[2] = new SqlParameter("@EndTime", endTime);
                pars[3] = new SqlParameter("@MerchantID", merchantId);
                var list = new DBHelper(Config.ConnectionString.SQLConnCMS).GetListSP<Finance>("SP_Finance_GetList", pars);
                if (list == null || list.Count <= 0)
                    return new List<Finance>();

                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new List<Finance>();
            }
        }

        public Finance FinanceGetInfoByID(int financeId)
        {
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@FinanceID", financeId);
                var list = new DBHelper(Config.ConnectionString.SQLConnCMS).GetInstanceSP<Finance>("SP_Finance_GetInfoByID", pars);
                if (list == null)
                    return new Finance();
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new Finance();
            }
        }

        public int Finance_Insert(Finance finance)
        {
            try
            {
                var pars = new SqlParameter[19];
                pars[0] = new SqlParameter("@CourseID", finance.CourseID);
                pars[1] = new SqlParameter("@StudentName", finance.StudentName);
                pars[2] = new SqlParameter("@Title", finance.Title);
                pars[3] = new SqlParameter("@Description", finance.Description);
                pars[4] = new SqlParameter("@Amount", finance.Amount);
                pars[5] = new SqlParameter("@AmountOfCouse", finance.AmountOfCourse);
                pars[6] = new SqlParameter("@PaymentDate", finance.PaymentDate);
                pars[7] = new SqlParameter("@DateOfBirth", finance.DateOfBirth);
                pars[8] = new SqlParameter("@Cmnd", finance.Cmnd);
                pars[9] = new SqlParameter("@DateBy", finance.DateBy);
                pars[10] = new SqlParameter("@IssueBy", finance.IssuedBy);
                pars[11] = new SqlParameter("@PhoneNumber", finance.PhoneNumber);
                pars[12] = new SqlParameter("@ParentPhoneNumber", finance.ParentPhoneNumber);
                pars[13] = new SqlParameter("@Email", finance.Email);
                pars[14] = new SqlParameter("@TeamSaleCode", finance.TeamSaleCode);
                pars[15] = new SqlParameter("@RegisterPlace", finance.RegisterPlace);
                pars[16] = new SqlParameter("@CreatedBy", finance.CreatedBy);
                pars[17] = new SqlParameter("@MerchantID", finance.MerchantID);
                pars[18] = new SqlParameter("@ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_Finance_Insert", pars);
                return Convert.ToInt32(pars[18].Value);
            }
            catch (Exception e)
            {
                NLogLogger.PublishException(e);
                return -99;
            }
        }

        public int Finance_Update(Finance finance)
        {
            try
            {
                var pars = new SqlParameter[20];
                pars[0] = new SqlParameter("@FinanceID", finance.FinanceID);
                pars[1] = new SqlParameter("@CourseID", finance.CourseID);
                pars[2] = new SqlParameter("@StudentName", finance.StudentName);
                pars[3] = new SqlParameter("@Title", finance.Title);
                pars[4] = new SqlParameter("@Description", finance.Description);
                pars[5] = new SqlParameter("@Amount", finance.Amount);
                pars[6] = new SqlParameter("@AmountOfCouse", finance.AmountOfCourse);
                pars[7] = new SqlParameter("@PaymentDate", finance.PaymentDate);
                pars[8] = new SqlParameter("@DateOfBirth", finance.DateOfBirth);
                pars[9] = new SqlParameter("@Cmnd", finance.Cmnd);
                pars[10] = new SqlParameter("@DateBy", finance.DateBy);
                pars[11] = new SqlParameter("@IssueBy", finance.IssuedBy);
                pars[12] = new SqlParameter("@PhoneNumber", finance.PhoneNumber);
                pars[13] = new SqlParameter("@ParentPhoneNumber", finance.ParentPhoneNumber);
                pars[14] = new SqlParameter("@Email", finance.Email);
                pars[15] = new SqlParameter("@TeamSaleCode", finance.TeamSaleCode);
                pars[16] = new SqlParameter("@RegisterPlace", finance.RegisterPlace);
                pars[17] = new SqlParameter("@CreatedBy", finance.CreatedBy);
                pars[18] = new SqlParameter("@MerchantID", finance.MerchantID);
                pars[19] = new SqlParameter("@ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_Finance_Update", pars);
                return Convert.ToInt32(pars[19].Value);
            }
            catch (Exception e)
            {
                NLogLogger.PublishException(e);
                return -99;
            }
        }

        #endregion
    }
}

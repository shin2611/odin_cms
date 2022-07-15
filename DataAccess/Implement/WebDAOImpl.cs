using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using DataAccess.Interface;
using DataAccess.Models;
using Utils;

namespace DataAccess.Implement
{
    public class WebDAOImpl : IWebDAO
    {
        public void CheckIPLocalInfo(long ip, int type, ref string countryCode, ref string countryName, ref string regionName, ref string cityName, ref int languageType)
        {
            try
            {
                var pars = new SqlParameter[7];
                pars[0] = new SqlParameter("@IP", ip);
                pars[1] = new SqlParameter("@Type", type);
                pars[2] = new SqlParameter("@Country_Code", SqlDbType.VarChar, 2) { Direction = ParameterDirection.Output };
                pars[3] = new SqlParameter("@Country_Name", SqlDbType.VarChar, 64) { Direction = ParameterDirection.Output };
                pars[4] = new SqlParameter("@Region_Name", SqlDbType.VarChar, 128) { Direction = ParameterDirection.Output };
                pars[5] = new SqlParameter("@City_Name", SqlDbType.VarChar, 128) { Direction = ParameterDirection.Output };
                pars[6] = new SqlParameter("@LanguageType", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnWEB).ExecuteNonQuerySP("SP_IP_Location_Info_WEB", pars);
                countryCode = Convert.ToString(pars[2].Value);
                countryName = Convert.ToString(pars[3].Value);
                regionName = Convert.ToString(pars[4].Value);
                cityName = Convert.ToString(pars[5].Value);
                languageType = Convert.ToInt32(pars[6].Value);
            }
            catch (Exception e)
            {
                NLogLogger.PublishException(e);
            }
        }

        public List<Languages> GetListLanguageType(int languageType, string code, ref int totalRow)
        {
            try
            {
                var pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@LanguageType", languageType);
                pars[1] = new SqlParameter("@Code", code);
                pars[2] = new SqlParameter("@TotalRow", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var list = new DBHelper(Config.ConnectionString.SQLConnWEB).GetListSP<Languages>("SP_LanguageType_GetList_WEB", pars);
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

        public List<Partner> GetListPartner(int partNerId, string partnerName, int languageType, ref int totalRow)
        {
            try
            {
                var pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@PartnerID", partNerId);
                pars[1] = new SqlParameter("@PartnerName", partnerName);
                pars[2] = new SqlParameter("@LanguageType", languageType);
                pars[3] = new SqlParameter("@TotalRow", SqlDbType.Int) { Direction = ParameterDirection.Output };
                var list = new DBHelper(Config.ConnectionString.SQLConnWEB).GetListSP<Partner>("SP_Partner_GetList_WEB", pars);
                if (list == null || list.Count <= 0)
                    return new List<Partner>();
                totalRow = Convert.ToInt32(pars[3].Value);
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                totalRow = 0;
                return new List<Partner>();
            }
        }

        public int SendContact(string contactName, string contactWant, string contactCompany, string emailContact, string message, int languageType)
        {
            try
            {
                var pars = new SqlParameter[7];
                pars[0] = new SqlParameter("@ContactName", contactName);
                pars[1] = new SqlParameter("@ContactWant", contactWant);
                pars[2] = new SqlParameter("@ContactCompany", contactCompany);
                pars[3] = new SqlParameter("@ContactEmail", emailContact);
                pars[4] = new SqlParameter("@Message", message);
                pars[5] = new SqlParameter("@LanguageType", languageType);
                pars[6] = new SqlParameter("@ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnWEB).ExecuteNonQuerySP("SP_Contact_Send_WEB", pars);
                return Convert.ToInt32(pars[6].Value);
            }
            catch (Exception e)
            {
                NLogLogger.PublishException(e);
                return -99;
            }
        }
    }
}

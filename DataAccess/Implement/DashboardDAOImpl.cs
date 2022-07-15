using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;
using DataAccess.Interface;
using Microsoft.Data.SqlClient;
using Utils;
using System.Data;


namespace DataAccess.Implement
{
    public class DashboardDAOImpl : IDashboardDAO
    {
        public List<Dashboard> GetList(string fromDate, string toDate)
        {
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@FromDate", fromDate);
                pars[1] = new SqlParameter("@ToDate", toDate);
                var list = new DBHelper(Config.ConnectionString.SQLConnWEB).GetListSP<Dashboard>("SP_Dashboard_Search", pars);
                if (list == null || list.Count <= 0)
                    return new List<Dashboard>();
                return list;
            }
            catch (Exception ex)
            {
                NLogLogger.LogInfo(ex.ToString());
                return new List<Dashboard>();
            }
        }
    }
}

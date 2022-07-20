using DataAccess.Interface;
using DataAccess.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Utils;

namespace DataAccess.Implement
{
    public class ScheduleDAOImpl : IScheduleDAO
    {
        public ResponseStatus InsertUpdateSchedule(Schedule sche, List<ScheduleDay> scheDay)
        {
            ResponseStatus res = new ResponseStatus();
            try
            {
                var pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@Id", sche.ScheduleId);
                pars[1] = new SqlParameter("@StartTime", sche.StartTime);
                pars[2] = new SqlParameter("@EndTime", sche.EndTime);
                pars[3] = new SqlParameter("@Status", sche.Status);
                pars[4] = new SqlParameter("@Response_Status", SqlDbType.Int) { Direction = ParameterDirection.Output };
                pars[5] = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("Sp_Schedule_InsertUpdate", pars);
                res.Status = Convert.ToInt32(pars[4].Value);
                res.Message = pars[5].Value.ToString();


                foreach (var item in scheDay)
                {
                    var param = new SqlParameter[5];
                    param[0] = new SqlParameter("@CheckingId", 0);
                    param[1] = new SqlParameter("@ScheduleId", Convert.ToInt32(pars[4].Value));
                    param[2] = new SqlParameter("@Day", item.Day);
                    param[3] = new SqlParameter("@Response_Status", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    param[4] = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar) { Direction = ParameterDirection.Output };
                    new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("Sp_ScheduleDay_InsertUpdate", pars);
                    res.Status = Convert.ToInt32(pars[3].Value);
                    res.Message = pars[4].Value.ToString();
                }

                return res;
            }
            catch (Exception e)
            {
                NLogLogger.PublishException(e);
                return null;
            }
        }
    }
}

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
    public class LevelOfClassDAOImpl : ILevelOfClassDAO
    {
        public ResponseStatus InsertUpdateLevelOfClass(LevelOfClass levelOfClass)
        {
            ResponseStatus res = new ResponseStatus();
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@Id", levelOfClass.Id);
                pars[1] = new SqlParameter("@Name", levelOfClass.Name);
                pars[2] = new SqlParameter("@Code", levelOfClass.Code);
                pars[3] = new SqlParameter("@Status", levelOfClass.Status);
                pars[4] = new SqlParameter("@Response_Status", SqlDbType.Int) { Direction = ParameterDirection.Output };
                pars[5] = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar) { Direction = ParameterDirection.Output };
                new DBHelper(Config.ConnectionString.SQLConnCMS).ExecuteNonQuerySP("SP_Functions_Update", pars);

                res.Status = Convert.ToInt32(pars[4].Value);
                res.Message = pars[5].Value.ToString();

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

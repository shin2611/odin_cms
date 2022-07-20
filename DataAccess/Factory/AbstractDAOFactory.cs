using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Interface;
using DataAccess.Models;

namespace DataAccess.Factory
{
    public abstract class AbstractDAOFactory
    {
        public static AbstractDAOFactory Instance()
        {
            try
            {
                return (AbstractDAOFactory)new ADODAOFactory();
            }
            catch (Exception ex)
            {

                throw new Exception("Couldn't create AbstractDAOFactory: ");
            }
        }

        public abstract IUsersDAO CreateUsersDAO();
        //public abstract IUsersLogDAO CreateUsersLogDAO();
        //public abstract IErrorLogDAO CreateErrorLogDAO();
        public abstract IFucntionsDAO CreateFunctionDAO();
        public abstract IUserRoleDAO CreateUserRoleDAO();
        public abstract ICMS CreateCMSDAO();
        public abstract IWebDAO CreateWEBDAO();
        public abstract IDashboardDAO CreateDashboardDAO();
        public abstract ILevelOfClassDAO CreateLevelOfClassDAO();
        public abstract IScheduleDAO CreateScheduleDAO();
    }
}

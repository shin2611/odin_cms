using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Implement;
using DataAccess.Interface;
namespace DataAccess.Factory
{
    public class ADODAOFactory : AbstractDAOFactory
    {
        public override IUsersDAO CreateUsersDAO()
        {
            return new UsersDAOImpl();
        }

        //public override IUsersLogDAO CreateUsersLogDAO()
        //{
        //    return new UsersLogDAOImpl();
        //}
        //public override IErrorLogDAO CreateErrorLogDAO()
        //{
        //    return new ErrorLogDAOImpl();
        //}
        public override IFucntionsDAO CreateFunctionDAO()
        {
            return new FunctionsDAOImpl();
        }
        public override IUserRoleDAO CreateUserRoleDAO()
        {
            return new UserRoleDAOImpl();
        }
        public override ICMS CreateCMSDAO()
        {
            return new CMSImpl();
        }

        public override IWebDAO CreateWEBDAO()
        {
            return new WebDAOImpl();
        }

        public override IDashboardDAO CreateDashboardDAO()
        {
            return new DashboardDAOImpl();
        }

        public override ILevelOfClassDAO CreateLevelOfClassDAO()
        {
            return new LevelOfClassDAOImpl();
        }

        public override IScheduleDAO CreateScheduleDAO()
        {
            return new ScheduleDAOImpl();
        }
    }
}

using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IUsersLogDAO
    {
        List<UsersLog> GetListUsersLog(string fromDate, string toDate, int userId, int functionId);
        int DeleteUsersLog(string fromDate, string toDate, int userId, int functionId);
        int InsertUsersLog(UsersLog log);
    }
}

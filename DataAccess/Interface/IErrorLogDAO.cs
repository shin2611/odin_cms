using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IErrorLogDAO
    {
        List<ErrorLog> GetErrorLog(string fromDate, string toDate);
        int Delete(string fromDate, string toDate);
    }
}

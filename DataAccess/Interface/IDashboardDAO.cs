using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;

namespace DataAccess.Interface
{
    public interface IDashboardDAO
    {
        List<Dashboard> GetList(string fromDate, string toDate);
    }
}

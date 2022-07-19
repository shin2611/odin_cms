using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interface
{
    public interface IScheduleDAO
    {
        ResponseStatus InsertUpdateSchedule(Schedule sche, List<ScheduleDay> scheDay);
        //ResponseStatus InsertUpdateScheduleDay();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;

namespace DataAccess.Interface
{
    public interface ICMS
    {
        List<Languages> GetListLanguage_CMS(int langType, int status, ref int totalRow);
        int INUP_Language_CMS(Languages language);

        List<Merchant> MerchantGetList(int merchantId);

        #region TEACHER
        List<Teacher> TeacherGetList(string keyword, int status, int merchantId);

        Teacher GetTeacherInfoByID(int teacherId);

        int INUP_Teacher_CMS(Teacher teacher);
        int Delete_Teacher(int teacherId);

        int UpdateTeacherStatus(int teacherId, int status);

        #endregion

        #region FINANCE
        List<Finance> FinanceGetList(string keyword, string startTime, string endTime, int merchantId);

        Finance FinanceGetInfoByID(int financeId);
        int Finance_Insert(Finance finance);
        int Finance_Update(Finance finance);
        
        #endregion

    }

}

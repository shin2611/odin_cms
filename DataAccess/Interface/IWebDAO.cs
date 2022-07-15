using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;

namespace DataAccess.Interface
{
    public interface IWebDAO
    {
        void CheckIPLocalInfo(long ip, int type, ref string countryCode, ref string countryName, ref string regionName, ref string cityName, ref int languageType);
        List<Languages> GetListLanguageType(int languageType, string code, ref int totalRow);
        int SendContact(string contactName, string contactWant, string contactCompany, string emailContact, string message, int languageType);
        List<Partner> GetListPartner(int partNerId, string partnerName, int languageType, ref int totalRow);
    }
}

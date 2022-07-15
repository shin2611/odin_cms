using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
   public class Partner
    {
        public int STT { get; set; }
        public int PartnerID { get; set; }
        public string PartnerName { get; set; }
        public string PartnerIcon { get; set; }
        public string PartnerDesc { get; set; }
        public int Status { get; set; }
        public int LanguageType { get; set; }
        public string Language { get; set; }
        public string MoreInfo { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }

    }
}

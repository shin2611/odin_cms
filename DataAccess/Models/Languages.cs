using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
   public class Languages
    {
        public int STT { get; set; }
        public int LanguageType { get; set; }
        public string Language { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
    }

}

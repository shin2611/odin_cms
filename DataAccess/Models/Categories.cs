using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Categories
    {
        public int STT { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDesc { get; set; }
        public int FatherID { get; set; }
        public string FatherName { get; set; }
        public int Status { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string Link { get; set; }
        public string FatherLink { get; set; }
        public int LanguageType { get; set; }
        public string Language { get; set; }
        public string LayoutType { get; set; }
    }
}

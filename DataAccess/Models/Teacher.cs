using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CmndNum { get; set; }
        public DateTime DateBy { get; set; }
        public string IssuedBy { get; set; }
        public string Cmnd_Path { get; set; }
        public string Cmnd_Before { get; set; }
        public string Cmnd_After { get; set; }
        public string Certificate { get; set; }
        public int MerchantID { get; set; }
        public string MerchantName { get; set; }
        public int Status { get; set; }
        public string ImagePath { get; set; }

    }
}

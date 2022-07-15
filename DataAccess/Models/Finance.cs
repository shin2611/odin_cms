using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
   public class Finance
    {
        public int STT { get; set; }
        public long FinanceID { get; set; }
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Amount { get; set; }
        public long AmountOfCourse { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
        public int MerchantID { get; set; }
        public string MerchantName { get; set; }
        public string CreatedBy { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Cmnd { get; set; }
        public DateTime DateBy { get; set; }
        public string IssuedBy { get; set; }
        public string Cmnd_Path { get; set; }
        public string PhoneNumber { get; set; }
        public string ParentPhoneNumber { get; set; }
        public string Email { get; set; }
        public string TeamSaleCode { get; set; }
        public int RegisterPlace { get; set; }

    }
}

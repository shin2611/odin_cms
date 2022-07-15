using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
   public class Merchant
    {
        public int MerchantID { get; set; }
        public string MerchantName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
   public class Accounts
    {
        public long AccountID { get; set; }
        public string AddressContract { get; set; }
        public DateTime CreateDate { get; set; }
        public string Facebook { get; set; }
        public string Tweeter { get; set; }
        public string Telegram { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
    }

    public class AccountInfo
    {
        public long AccountID { get; set; }
        public string AddressContract { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastLogin { get; set; }
        public string LastIPLogin { get; set; }
    }
}

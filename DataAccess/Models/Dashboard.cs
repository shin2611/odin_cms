using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Dashboard
    {
        public DateTime ClaimDate { get; set; }
        public long NRU { get; set; }
        public int DAU { get; set; }
        public long TotalClaimSeed { get; set; }
        public long TotalClaimPrivate { get; set; }
    }
}

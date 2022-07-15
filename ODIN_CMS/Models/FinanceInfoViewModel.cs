using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;

namespace ODIN_CMS.Models
{
    public class FinanceInfoViewModel
    {
        public Finance Finance { get; set; }
        public List<Merchant> ListMerchant { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
namespace ODIN_CMS.Models
{
    public class UserInfoViewModel
    {
        public Users Users { get; set; }
        public List<Groups> ListGroups { get; set; }
        public List<Merchant> ListMerchant { get; set; }
    }
}

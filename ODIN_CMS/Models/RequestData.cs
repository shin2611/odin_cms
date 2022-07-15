using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ODIN_CMS.Models
{
    public class RequestData
    {

    }

    public class LoginModel : RequestData
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Otp")]
        public string Otp { get; set; }

        public string Captcha { get; set; }
        public string Token { get; set; }

        public int Type { get; set; }
    }

    public class Accounts
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
   
        //public int ResponseCode { get; set; }
        // public string ResponseDesc { get; set; }
    }
    public class ChangePassWord
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "OldPass")]
        public string OldPass { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "NewPass")]
        public string NewPass { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPass")]
        public string ConfirmNewPass { get; set; }

        [Required]
        [Display(Name = "Otp")]
        public string Otp { get; set; }

    }
}
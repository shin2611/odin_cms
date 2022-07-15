using System.ComponentModel.DataAnnotations;

namespace ODIN_CMS.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu cũ")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu cũ")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu mới")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mã otp")]
        [Display(Name = "OTP")]
        public string Otp { get; set; }
    }
}
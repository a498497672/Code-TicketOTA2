using System.ComponentModel.DataAnnotations;

namespace Ticket.Model.Model.OtaBusinessUser
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "请填写用户名.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "请填写用户密码.")]
        public string PassWord { get; set; }
        [Required(ErrorMessage = "请填写验证码.")]
        public string Code { get; set; }
    }
}

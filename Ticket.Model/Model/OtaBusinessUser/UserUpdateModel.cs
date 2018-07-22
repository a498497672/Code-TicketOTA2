using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Model.Model.OtaBusinessUser
{
    public class UserUpdateModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "请填写旧密码.")]
        public string PassWord { get; set; }
        [Required(ErrorMessage = "请填写新密码.")]
        public string NowPassWord { get; set; }
    }
}

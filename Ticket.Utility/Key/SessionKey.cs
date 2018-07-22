using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Utility.Key
{
    public class SessionKey
    {
        /// <summary>
        /// 售票系统登录的cookie
        /// </summary>
        public const string SaleUserLoginCookie = "SaleUserLoginCookie";
        /// <summary>
        /// 售票系统用户信息session
        /// </summary>
        public const string UserInfo = "Session_Manage_UserInfo";
        /// <summary>
        /// 内部系统SESSION
        /// </summary>
        public const string ManagerUserInfo = "ManagerUserInfo";

        /// <summary>
        /// 票务系统登录验证码
        /// </summary>
        public const string ManagerUserLoginCode = "ManagerUserLoginCode";
        /// <summary>
        /// 票务系统登录的cookie
        /// </summary>
        public const string ManagerUserLoginCookie = "ManagerUserLoginCookie";

        /// <summary>
        /// 内部系统COOKIE加密解密秘钥
        /// </summary>
        public const string ManagerUserLoginCookieKey = "fengjing";

        public const string CloudUserInfo = "Session_Cloud_UserInfo";
    }
}

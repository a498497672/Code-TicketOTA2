using System.Web.Security;

namespace Ticket.Utility.Helpers
{
    public class Md5HashHelper
    {
        public static string HashPassword(string str)
        {
            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            return sign;
        }
    }
}

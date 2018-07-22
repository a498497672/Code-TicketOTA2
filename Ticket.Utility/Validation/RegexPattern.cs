namespace Ticket.Utility.Validation
{
    /// <summary>
    /// 功能描述 : 常用正则表达式
    /// </summary>
    public class RegexPattern
    {
        public const string ALPHA = "[^a-zA-Z]";
        public const string ALPHA_NUMERIC = "[^a-zA-Z0-9]";
        public const string ALPHA_NUMERIC_SPACE = @"[^a-zA-Z0-9\s]";
        //用户名
        public const string USERNAME = "^[a-zA-Z][a-zA-Z0-9_]{3,15}$";
        //邮箱
        public const string EMAIL = @"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";
        //手机号码
        public const string CELLPHONE = @"^(13[0-9]|14[57]|15[012356789]|17[678]|18[0123456789])[0-9]{8}$";
        //验证电话号码（包含国际区号、本地区号、电话、分机号，分别用‘-’分隔，分机号使用‘-’或‘,’分隔）
        public const string TELEPHONE = @"^[\+]?[\d]{1,4}-[\d]{1,5}-[\d]{1,9}([-|,][0-9]{1,4})?$";
        //验证电话号码中的国际区号
        public const string COUNTRYCODE = @"^[\+]?[\d]{1,4}$";
        //验证电话号码中的本地区号
        public const string AREACODE = @"^[\d]{1,5}$";
        //验证电话号码（包含分机号）
        public const string PHONECODE = @"^[\d]{1,9}([-|,][0-9]{1,4})?$";
        //验证企业网址
        public const string WEBSITE = @"([\w-]+\.)+[\w-]+.([^a-z])(/[\w-: ./?%&=]*)?|[a-zA-Z\-\.][\w-]+.([^a-z])(/[\w-: ./?%&=]*)?|(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
        //验证货币(注：验证货)
        public const string MONEY = @"^\d+(\.\d{2})?$";
        //验证QQ
        public const string QQ = @"^\d{5,12}$";
        //验证是否包含html标记
        public const string HTMLTAG = @"<[^<]*>";
        //验证是否数字
        public const string NUMBER = @"^\d+$";

        public const string EMBEDDED_CLASS_NAME_MATCH = "(?<=^_).*?(?=_)";
        public const string EMBEDDED_CLASS_NAME_REPLACE = "^_.*?_";
        public const string EMBEDDED_CLASS_NAME_UNDERSCORE_MATCH = "(?<=^UNDERSCORE).*?(?=UNDERSCORE)";
        public const string EMBEDDED_CLASS_NAME_UNDERSCORE_REPLACE = "^UNDERSCORE.*?UNDERSCORE";
        public const string GUID = "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}";
        public const string IP_ADDRESS = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        public const string LOWER_CASE = @"^[a-z]+$";
        public const string NUMERIC = "[^0-9]";
        public const string SOCIAL_SECURITY = @"^\d{3}[-]?\d{2}[-]?\d{4}$";
        public const string SQL_EQUAL = @"\=";
        public const string SQL_GREATER = @"\>";
        public const string SQL_GREATER_OR_EQUAL = @"\>.*\=";
        public const string SQL_IS = @"\x20is\x20";
        public const string SQL_IS_NOT = @"\x20is\x20not\x20";
        public const string SQL_LESS = @"\<";
        public const string SQL_LESS_OR_EQUAL = @"\<.*\=";
        public const string SQL_LIKE = @"\x20like\x20";
        public const string SQL_NOT_EQUAL = @"\<.*\>";
        public const string SQL_NOT_LIKE = @"\x20not\x20like\x20";

        public const string STRONG_PASSWORD =
            @"(?=^.{8,255}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*";

        public const string UPPER_CASE = @"^[A-Z]+$";
        public const string URL = @"^^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_=]*)?$";
        public const string US_CURRENCY = @"^\$(([1-9]\d*|([1-9]\d{0,2}(\,\d{3})*))(\.\d{1,2})?|(\.\d{1,2}))$|^\$[0](.00)?$";


        public const string ID_CARD = @"(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)";
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Ticket.Utility.Validation
{
    public static class RegexValidation
    {
        #region Validation
        /// <summary>
        /// 验证指定的字符串中是否仅包含字母
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串中是否仅包含字母; 否则, <c>false</c>.
        /// </returns>
        public static bool IsAlpha(this string evalString)
        {
            return !Regex.IsMatch(evalString, RegexPattern.ALPHA);
        }

        /// <summary>
        /// 验证指定的字符串中是否仅包含字母和数字
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串中是否仅包含字母和数字; 否则, <c>false</c>.
        /// </returns>
        public static bool IsAlphaNumeric(this string evalString)
        {
            return !Regex.IsMatch(evalString, RegexPattern.ALPHA_NUMERIC);
        }

        /// <summary>
        /// 验证指定的字符串中是否仅包含字母和数字
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <param name="allowSpaces">是否允许空格</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串中是否仅包含字母和数字; 否则, <c>false</c>.
        /// </returns>
        public static bool IsAlphaNumeric(this string evalString, bool allowSpaces)
        {
            if (allowSpaces)
                return !Regex.IsMatch(evalString, RegexPattern.ALPHA_NUMERIC_SPACE);
            return IsAlphaNumeric(evalString);
        }

        /// <summary>
        /// 验证指定的字符串中是否为有效的用户名，以字母开头，只允许含有字母，数字，下划线，长度4-16
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串为有效的用户名; 否则, <c>false</c>.
        /// </returns>
        public static bool IsUserName(this string evalString)
        {
            return Regex.IsMatch(evalString, RegexPattern.USERNAME);
        }

        /// <summary>
        /// 验证指定的字符串中是否仅包含数字
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串中是否仅包含数字; 否则, <c>false</c>.
        /// </returns>
        public static bool IsNumeric(this string evalString)
        {
            return Regex.IsMatch(evalString, RegexPattern.NUMERIC);
        }

        /// <summary>
        /// 验证指定的字符串中是否为有效的Email地址
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串中是否为有效的Email地址; 否则, <c>false</c>.
        /// </returns>
        public static bool IsEmail(this string emailAddressString)
        {
            return Regex.IsMatch(emailAddressString, RegexPattern.EMAIL);
        }

        /// <summary>
        /// 验证指定的字符串中是否为有效的手机号码
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串中是否为有效的手机号码; 否则, <c>false</c>.
        /// </returns>
        public static bool IsCellPhone(this string cellPhoneString)
        {
            return Regex.IsMatch(cellPhoneString, RegexPattern.CELLPHONE);
        }

        /// <summary>
        /// 验证指定的字符串中是否为有效的电话号码
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串中是否为有效的电话号码; 否则, <c>false</c>.
        /// </returns>
        public static bool IsTelePhone(this string telePhoneString)
        {
            return Regex.IsMatch(telePhoneString, RegexPattern.TELEPHONE);
        }

        /// <summary>
        ///  验证电话号码（包含分机号）
        /// </summary>
        /// <param name="phoneCodeString"></param>
        /// <returns></returns>
        public static bool IsPhoneCode(this string phoneCodeString)
        {
            return Regex.IsMatch(phoneCodeString, RegexPattern.PHONECODE);
        }

        /// <summary>
        /// 验证指定的字符串中是否为有效的QQ号码
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串中是否为有效的QQ号码; 否则, <c>false</c>.
        /// </returns>
        public static bool IsQQ(this string qqString)
        {
            if (Regex.IsMatch(qqString, RegexPattern.NUMERIC) && qqString.Length <= 11)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 验证指定的字符串中是否仅包含小写字符
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串中是否仅包含小写字符; 否则, <c>false</c>.
        /// </returns>
        public static bool IsLowerCase(this string inputString)
        {
            return Regex.IsMatch(inputString, RegexPattern.LOWER_CASE);
        }

        /// <summary>
        /// 验证指定的字符串中是否仅包含大写字符
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串中是否仅包含大写字符; 否则, <c>false</c>.
        /// </returns>
        public static bool IsUpperCase(this string inputString)
        {
            return Regex.IsMatch(inputString, RegexPattern.UPPER_CASE);
        }

        /// <summary>
        /// 验证指定的字符串中是否为有效的Guid
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串中是否为有效的Guid; 否则, <c>false</c>.
        /// </returns>
        public static bool IsGuid(this string guid)
        {
            return Regex.IsMatch(guid, RegexPattern.GUID);
        }

        /// <summary>
        /// 验证指定的字符串中是否为有效的IP地址
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串中是否为有效的IP地址; 否则, <c>false</c>.
        /// </returns>
        public static bool IsIPAddress(this string ipAddress)
        {
            return Regex.IsMatch(ipAddress, RegexPattern.IP_ADDRESS);
        }

        /// <summary>
        /// 验证指定的字符串中是否为有效的URL
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串中是否为有效的URL; 否则, <c>false</c>.
        /// </returns>
        public static bool IsURL(this string url)
        {
            return Regex.IsMatch(url, RegexPattern.URL);
        }

        /// <summary>
        /// 验证指定的字符串中是否为有效的强密码串
        /// </summary>
        /// <param name="evalString">待验证的字符串.</param>
        /// <returns>
        /// 	<c>true</c> 如果指定的字符串中是否为有效的强密码串; 否则, <c>false</c>.
        /// </returns>
        public static bool IsStrongPassword(this string password)
        {
            return Regex.IsMatch(password, RegexPattern.STRONG_PASSWORD);
        }

        /// <summary>
        /// 验证身份证是否合法
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        public static bool IsIdCard(this string idCard)
        {
            return Regex.IsMatch(idCard, RegexPattern.ID_CARD);
        }

        /// <summary>
        /// 字符串是否可转换为数值类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsStringNumeric(this string str)
        {
            double result;
            return (double.TryParse(str, NumberStyles.Float, NumberFormatInfo.CurrentInfo, out result));
        }

        public static void ArgumentConditionTrue(bool condition, string parameterName, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message, parameterName);
            }
        }

        public static void ArgumentNotNull(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void ArgumentNotNullOrEmpty(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void ArgumentNotNullOrEmpty<T>(ICollection<T> collection, string parameterName, string message)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            if (collection.Count == 0)
            {
                throw new ArgumentException(message, parameterName);
            }
        }

        public static void ArgumentNotNullOrEmpty(ICollection collection, string parameterName, string message)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            if (collection.Count == 0)
            {
                throw new ArgumentException(message, parameterName);
            }
        }


        #endregion
    }
}

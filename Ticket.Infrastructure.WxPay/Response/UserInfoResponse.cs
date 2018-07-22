namespace Ticket.Infrastructure.WxPay.Response
{
    /// <summary>
    /// 微信用户信息
    /// </summary>
    public class UserInfoResponse
    {
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 用户的昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 用户头像,最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），
        /// 用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
        /// </summary>
        public string HeadImgUrl { get; set; }
    }
}

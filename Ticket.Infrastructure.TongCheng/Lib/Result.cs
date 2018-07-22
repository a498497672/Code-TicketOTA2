namespace Ticket.Infrastructure.TongCheng.Lib
{
    public class Result<T>
    {
        /// <summary>
        /// 执行失败返回
        /// </summary>
        /// <param name="description"></param>
        /// <param name="tts"></param>
        /// <returns></returns>
        public static Result<T> FailResult(string response)
        {
            var result = new Result<T>
            {
                Status = false,
                Response = response
            };
            return result;
        }

        /// <summary>
        /// 执行成功返回
        /// </summary>
        /// <param name="description"></param>
        /// <param name="tts"></param>
        /// <returns></returns>
        public static Result<T> SuccessResult(T data)
        {
            var result = new Result<T>
            {
                Status = true,
                Data = data
            };
            return result;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 返回
        /// </summary>
        public string Response { get; set; }
    }

    public class Result
    {
        /// <summary>
        /// 执行失败返回
        /// </summary>
        /// <param name="description"></param>
        /// <param name="tts"></param>
        /// <returns></returns>
        public static Result FailResult(string response)
        {
            var result = new Result
            {
                Status = false,
                Response = response
            };
            return result;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 返回
        /// </summary>
        public string Response { get; set; }
    }
}

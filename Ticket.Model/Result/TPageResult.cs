using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Model.Result
{
    public class TPageResult<T> : TResult
    {
        public TPageResult() : base()
        {
            this.Data = new List<T>();
            this.Count = 0;
        }

        public TPageResult(bool success, string message, List<T> data, int count) : base(success, message)
        {
            this.Success = success;
            this.Message = message;
            this.Data = data;
            this.Count = count;
        }

        /// <summary>
        /// 总条数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Data { get; set; }

        public TPageResult<T> CommonResult(bool success, string message, List<T> data, int count)
        {
            this.Success = success;
            this.Message = message;
            this.Data = data;
            this.Count = count;
            return this;
        }

        public TPageResult<T> SuccessResult(List<T> data, int count, string message = "成功")
        {
            return CommonResult(true, message, data, count);
        }

        public TPageResult<T> FailureResult(List<T> data, string message = "失败")
        {
            return CommonResult(false, message, data, 0);
        }
    }
}

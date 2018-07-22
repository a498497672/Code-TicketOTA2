using FJZL.Msg;
using System.Threading.Tasks;
using Ticket.Utility.Validation;

namespace Ticket.Utility.Helpers
{
    /// <summary>
    /// 调用公司短信接口
    /// </summary>
    public class SesHelper
    {
        /// <summary>
        /// 发送手机短信
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static bool Send(string mobile, string content)
        {
            if (!RegexValidation.IsCellPhone(mobile))
            {
                return false;
            }
            return true;
            //Task<string> task = new Task<string>(() =>
            //{
            //    string msg = MsgManage.SendSMSNew(mobile, content);
            //    return msg;
            //});
            ////启动任务,并安排到当前任务队列线程中执行任务(System.Threading.Tasks.TaskScheduler)
            //task.Start();
            ////等待任务的完成执行过程。
            //task.Wait();
            //long resultMsg = 0;
            //long.TryParse(task.Result, out resultMsg);
            //return resultMsg > 0;
        }
    }
}

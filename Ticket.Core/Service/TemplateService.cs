using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Utility.Helpers;

namespace Ticket.Core.Service
{
    public class TemplateService
    {
        protected string JNTemplatePath
        {
            get
            {
                string path;
                if (System.Web.HttpRuntime.AppDomainAppPath.EndsWith("\\"))
                    path = string.Concat(System.Web.HttpRuntime.AppDomainAppPath, "JNTemplate\\");
                else
                    path = string.Concat(System.Web.HttpRuntime.AppDomainAppPath, "\\JNTemplate\\");

                return path;
            }
        }

        /// <summary>
        /// 返回填充模板后是内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetJNTemplateData(string path, object data)
        {
            string finalContent = null;
            path = path.TrimStart('\\');
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            finalContent = new JnTemplateHelper(context).GetJNTemplateData(JNTemplatePath + path, data, null);
            return finalContent;
        }
    }
}

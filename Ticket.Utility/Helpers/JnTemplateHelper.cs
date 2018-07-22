using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ticket.Utility.Helpers
{
    public class JnTemplateHelper
    {
        //private HttpContextBase _context;
        private HttpContext _context;
        public JnTemplateHelper(HttpContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// 填充模板并呈现
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        public void SetJNTemplateData(string path, object data, object baseData)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("请设置模版路径");
            else if (!path.EndsWith(".htm") && !path.EndsWith(".html"))
                throw new Exception("未设置模版路径或模版路径不是以.htm 或html结尾");
            //if (data == null)
            //    throw new Exception("请设置数据实例");
            JinianNet.JNTemplate.ITemplate template = JinianNet.JNTemplate.Engine.LoadTemplate(path);
            template.Context.TempData.Push("data", data);
            template.Context.TempData.Push("basedata", baseData);
            template.Render(this._context.Response.Output);
            this._context.Response.End();
        }

        /// <summary>
        /// 填充模板并呈现，并返回当前填充后的所有内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="finalContent"></param>
        public void SetJNTemplateData(string path, object data, object baseData, out string finalContent)
        {
            finalContent = null;

            if (string.IsNullOrEmpty(path))
                throw new Exception("请设置模版路径");
            else if (!path.EndsWith(".htm") && !path.EndsWith(".html"))
                throw new Exception("未设置模版路径或模版路径不是以.htm 或html结尾");
            //if (data == null)
            //    throw new Exception("请设置数据实例");
            JinianNet.JNTemplate.ITemplate template = JinianNet.JNTemplate.Engine.LoadTemplate(path);
            template.Context.TempData.Push("data", data);
            template.Context.TempData.Push("basedata", baseData);
            template.Render(this._context.Response.Output);

            JinianNet.JNTemplate.Template temp = template as JinianNet.JNTemplate.Template;
            finalContent = temp.Render();

            this._context.Response.End();
        }

        /// <summary>
        /// 返回填充模板后是内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetJNTemplateData(string path, object data, object baseData)
        {
            string finalContent = null;

            if (string.IsNullOrEmpty(path))
                throw new Exception("请设置模版路径");
            else if (!path.EndsWith(".htm") && !path.EndsWith(".html"))
                throw new Exception("未设置模版路径或模版路径不是以.htm 或html结尾");
            //if (data == null)
            //    throw new Exception("请设置数据实例");
            JinianNet.JNTemplate.ITemplate template = JinianNet.JNTemplate.Engine.LoadTemplate(path);
            template.Context.TempData.Push("data", data);
            template.Context.TempData.Push("basedata", baseData);

            JinianNet.JNTemplate.Template temp = template as JinianNet.JNTemplate.Template;
            finalContent = temp.Render();
            return finalContent;
        }


    }
}

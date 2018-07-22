using System.Web.Mvc;
using Ticket.Model.Model.Report;
using Ticket.SaleTicketPlatform.App_Start;
using Ticket.SaleTicketPlatform.Application;
using Ticket.Utility.Exceptions;

namespace Ticket.SaleTicketPlatform.Controllers
{
    public class ReportController : BaseController
    {
        private readonly ReportPrintFacadeService _reportPrintFacadeService;

        public ReportController(ReportPrintFacadeService reportPrintFacadeService, EnterpriseUserFacadeService enterpriseUserFacadeService) : base(enterpriseUserFacadeService)
        {
            _reportPrintFacadeService = reportPrintFacadeService;
        }

        /// <summary>
        /// 获取日结报表数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDailyReportStatistics()
        {
            var data = _reportPrintFacadeService.ReportStatistics();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 打印日结报表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PrintDaily(PrintReportModel model)
        {
            if (!ModelState.IsValid)
            {
                var message = ModelState.BuildErrorMessage();
                throw new SimpleBadRequestException(message);
            }
            var data = _reportPrintFacadeService.Daily(model.PrintKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
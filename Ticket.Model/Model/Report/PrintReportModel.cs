using System.ComponentModel.DataAnnotations;

namespace Ticket.Model.Model.Report
{
    public class PrintReportModel
    {
        [Required(ErrorMessage = "指定打印机名称不能为空.")]
        public string PrintKey { get; set; }
    }
}

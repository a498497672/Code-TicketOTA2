using System.ComponentModel.DataAnnotations;

namespace Ticket.Model.Model.Order
{
    public class PrintModel
    {
        public int OrderDetailId { get; set; }
        [Required(ErrorMessage = "指定打印机名称不能为空.")]
        public string PrintKey { get; set; }
    }
}

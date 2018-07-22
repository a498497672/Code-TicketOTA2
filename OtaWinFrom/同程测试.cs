using FengjingSDK461.Core;
using FengjingSDK461.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtaWinFrom
{
    public partial class 同程测试 : Form
    {
        private readonly TicketGateway ticketGateway = new TicketGateway(OtaType.TongCheng);
        public 同程测试()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var str = textBox1.Text;


            ticketGateway.Post(str, "http://localhost:51662/api/tongCheng/handler2");
        }
    }
}

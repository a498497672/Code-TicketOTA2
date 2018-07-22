using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ticket.Infrastructure.KouDaiLingQian.Core;
using Ticket.Utility.Helpers;

namespace OtaWinFrom
{
    public partial class 支付 : Form
    {
        public 支付()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DeviceActivation.Run();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = MicroOrder.Pay("0.01", textBox2.Text, OrderHelper.GenerateOrderNo()).Message;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = OrderQuery.Query(textBox3.Text).ReturnMessage;
        }
    }
}

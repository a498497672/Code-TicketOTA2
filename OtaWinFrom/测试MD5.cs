using FengjingSDK461.Helpers;
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
    public partial class 测试MD5 : Form
    {
        public 测试MD5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < 500; i++)
            {
                var md5 = Md5Helper.Md5Encrypt32(Guid.NewGuid().ToString(), "22");
                textBox1.Text = "\r\n" + md5 + " 长度：" + md5.Length + textBox1.Text;
            }


        }
    }
}

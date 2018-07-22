using DocomSDK.Ticket.Data;
using DocomSDK.Ticket.Enum;
using FengjingSDK461.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ticket.Utility.Extensions;
using Ticket.Utility.Key;

namespace OtaWinFrom
{
    public partial class 闸机模拟器 : Form
    {
        public 闸机模拟器()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(CheckTicket)
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void CheckTicket()
        {
            var number = SecurityExtension.DesEncrypt(textBox1.Text, DesKey.QrCodeKey);
            CheckTicket_Object obj = new CheckTicket_Object
            {
                SensorSource = "number30",
                Number = number,
                Device = new DeviceStatus
                {
                    DeviceName = comboBox1.Text.Split(',')[0],
                    Startup = DateTime.Now
                }
            };
            var result = 闸机类.Ticket_CheckTicket(obj);
            textBox2.Text += "\r\n\r\n 开始验票。。。。。。。";
            textBox2.Text += "\r\n\r\n 验票结果：" + result.Description;

            if (result.State == Result_Code.OK && result.Data.Code == OperationCode.VerifyPass)
            {
                textBox2.Text += "\r\n\r\n 正在提交过闸。。。。。。。。。。";
                var result2 = 闸机类.Ticket_SubmitWalkPast(new SubmitWalkPast_Object
                {
                    Session = result.Data.Session,
                    Ticket = result.Data.TicketData,
                    Count = 1,
                    EventDate = DateTime.Now,
                    Device = new DeviceStatus
                    {
                        DeviceName = result.Data.Session.DeviceID,
                        Startup = DateTime.Now
                    }
                });
                if (result2.State == Result_Code.OK)
                {
                    textBox2.Text += "\r\n\r\n 过闸结果：" + result2.Description;
                    var count = 1;
                    bool isTrue = result2.Data.Code == OperationCode.VerifyPass;
                    while (isTrue)
                    {
                        count++;
                        textBox2.Text += "\r\n\r\n 第" + count + "次，提交过闸。。。。。。。。。。";
                        var result3 = 闸机类.Ticket_SubmitWalkPast(new SubmitWalkPast_Object
                        {
                            Session = result2.Data.Session,
                            Ticket = result2.Data.TicketData,
                            Count = 1,
                            EventDate = DateTime.Now,
                            Device = new DeviceStatus
                            {
                                DeviceName = result2.Data.Session.DeviceID,
                                Startup = DateTime.Now
                            }
                        });
                        if (result3.State == Result_Code.OK && result3.Data.Code == OperationCode.VerifyPass)
                        {
                            result2 = result3;
                        }
                        else
                        {
                            isTrue = false;
                        }
                        textBox2.Text += "\r\n\r\n 过闸结果：" + result3.Description;
                    }

                }
                else
                {
                    textBox2.Text += "\r\n\r\n 过闸结果：" + result2.Description;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            if (form.ShowDialog() == DialogResult.OK)//对话框返回值为ok时运行
            {
                form.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            售取票机 form = new 售取票机();
            if (form.ShowDialog() == DialogResult.OK)//对话框返回值为ok时运行
            {
                form.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }
    }
}

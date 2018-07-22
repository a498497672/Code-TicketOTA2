using DocomSDK.TVM;
using FengjingSDK461.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace OtaWinFrom
{
    public partial class 售取票机 : Form
    {
        public 售取票机()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private delegate void InvokeHandler();
        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(CreateSingleOrder)
            {
                IsBackground = true
            };
            thread.Start();
        }

        public void SetMsg(string msg)
        {
            textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + ": " + msg + "\r\n" + textBox1.Text;
        }

        private void GetPrdoct()
        {
            var result = 闸机类.DeviceIni(new TVMDevice
            {
                DeviceKey = comboBox1.Text.Split(',')[0]
            });

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("产品名称");
            dataTable.Columns.Add("产品id");
            dataTable.Columns.Add("销售价格");
            dataTable.Columns.Add("购买人数");
            dataTable.Rows.Clear();
            if (result.Data != null)
            {
                foreach (var row in result.Data.tvmSpotsList[0].TicketList)
                {
                    DataRow drow = dataTable.NewRow();
                    drow["产品名称"] = row.TicketName;
                    drow["产品id"] = row.TicketID;
                    drow["销售价格"] = row.TicketPrice;
                    drow["购买人数"] = 1;
                    dataTable.Rows.Add(drow);
                }
            }
            Invoke(new InvokeHandler(delegate ()
            {
                dataGridView1.DataSource = dataTable;
            }));
            SetMsg(string.Format("获取订单：{0} {1} \r\n", result.ResultCode, result.Message));
            if (result.ResultCode == 0)
            {
                SetMsg("获取订单： \r\n" + JsonHelper.ObjectToJson(result.Data.tvmSpotsList));
            }
        }

        /// <summary>
        /// 创建单个产品订单
        /// </summary>
        private void CreateSingleOrder()
        {
            var deviceKey = comboBox1.Text;
            ////道控支付类型 0：支付宝 1：微信
            QuickPayment_Object request = new QuickPayment_Object
            {
                Device = new TVMDevice
                {
                    DeviceKey = deviceKey.Split(',')[0]
                },
                PayData = new TVMPayInfo
                {
                    payType = comboBox3.Text == "支付宝" ? "0" : "1",
                    ScannerPayCode = textBox3.Text,
                    postOrder = new List<ProductInfo>(),
                    merchantCode = "",
                    terminalNo = "",
                    TerminalType = ""
                }
            };

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == true)
                {
                    request.PayData.postOrder.Add(new ProductInfo
                    {
                        ProductCount = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value),
                        ProductID = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value),
                        ProductName = dataGridView1.Rows[i].Cells[0].Value.ToString(),
                        ProductPrice = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value)
                    });
                }
            }
            if (request.PayData.postOrder.Count <= 0)
            {
                MessageBox.Show("请选择你要购买的产品");
                return;
            }
            var result = 闸机类.QuickPayment(request);
            SetMsg(string.Format("支付订单：{0} {1} \r\n", result.ResultCode, result.Message));
            if (result.ResultCode == 0)
            {
                var requertQueryPay = 闸机类.QueryPay(request);
                SetMsg(string.Format("查询支付：{0} {1} \r\n", requertQueryPay.ResultCode, requertQueryPay.Message));

                if (requertQueryPay.ResultCode == 0)
                {
                    SetMsg("查询支付详情： \r\n" + JsonHelper.ObjectToJson(requertQueryPay.Data));
                }

                //SetMsg("支付成功： \r\n" + JsonHelper.ObjectToJson(result));
                SetMsg("支付成功： 订单号 " + result.Data.m_PayPassOrderID + "\r\n");

                var saleResult = 闸机类.SaleTicket(new QuickPayment_Object
                {
                    Device = new TVMDevice(),
                    PayData = new TVMPayInfo
                    {
                        m_PayPassOrderID = result.Data.m_PayPassOrderID
                    }
                });
                SetMsg(string.Format("获取订单详情：{0} {1} \r\n", saleResult.ResultCode, saleResult.Message));
                if (saleResult.ResultCode == 0)
                {
                    SetMsg("获取订单详情： \r\n" + JsonHelper.ObjectToJson(saleResult.Data));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(GetTicketOrder)
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void GetTicketOrder()
        {
            //取票类型 1、凭证码。2、身份证。3、扫码
            var result = 闸机类.GetTicketOrder(new TicketOrder_Object
            {
                Device = new TVMDevice(),
                MsgType = comboBox2.Text == "凭证码" ? 1 : 2,
                Msg = textBox4.Text

            });
            SetMsg(string.Format("取票：{0} {1} \r\n", result.ResultCode, result.Message));
            if (result.ResultCode == 0)
            {
                SetMsg(string.Format("取票数据：{0} \r\n", JsonHelper.ObjectToJson(result.Data)));
                if (result.Data.Length <= 0)
                {
                    return;
                }

                var TicketList = new List<TVMTicketInfo>();
                foreach (var row in result.Data)
                {
                    TicketList.Add(new TVMTicketInfo
                    {
                        Remarks = row.Remarks
                    });
                }
                SetMsg("开始核销订单。。。 \r\n");
                var verify = 闸机类.VerifyTicket(new VerifyTicket_Object
                {
                    Device = new TVMDevice(),
                    TicketList = TicketList
                });
                SetMsg(string.Format("核销订单：{0} {1} \r\n", verify.ResultCode, verify.Message));
                if (verify.ResultCode == 0)
                {
                    SetMsg("核销结果： \r\n" + JsonHelper.ObjectToJson(verify.Data));
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(GetPrdoct)
            {
                IsBackground = true
            };
            thread.Start();
        }
    }
}

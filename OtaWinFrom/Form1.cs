using FengjingSDK461.Helpers;
using FengjingSDK461.Model.Request;
using FengjingSDK461.Model.Response;
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
using Ticket.Utility.Helpers;

namespace OtaWinFrom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            textTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
            textBox4.Text = OrderHelper.GenerateOrderNo();
            textBox15.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
        }
        private delegate void InvokeHandler();
        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(UpdataUIStatusAsync)
            {
                IsBackground = true
            };
            thread.Start();


        }

        private void UpdataUIStatusAsync()
        {
            int type = 0;
            int productId = 0;
            int currentPage = 0;
            int pageSize = 0;
            if (comboBox1.Text == "分页")
            {
                type = 1;
                if (string.IsNullOrEmpty(textBox13.Text))
                {
                    MessageBox.Show("请填写当前页数");
                    return;
                }
                currentPage = Convert.ToInt32(textBox13.Text);
                if (string.IsNullOrEmpty(textBox14.Text))
                {
                    MessageBox.Show("请填写每页记录数");
                    return;
                }
                pageSize = Convert.ToInt32(textBox14.Text);
            }
            else if (comboBox1.Text == "获取单个产品")
            {
                type = 2;
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("请填写产品id");
                    return;
                }
                productId = Convert.ToInt32(textBox1.Text);
            }

            ProductQueryRequest request = new ProductQueryRequest
            {
                Body = new Product
                {
                    Type = type,
                    ProductId = productId,
                    CurrentPage = currentPage,
                    PageSize = pageSize
                }
            };
            var result = Class1.GetProduct(request);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("产品名称");
            dataTable.Columns.Add("产品id");
            dataTable.Columns.Add("景区名称");
            dataTable.Columns.Add("销售价格");
            dataTable.Columns.Add("购买人数");
            dataTable.Rows.Clear();
            if (result.Body != null)
            {
                foreach (var row in result.Body.ProductList)
                {
                    DataRow drow = dataTable.NewRow();
                    drow["产品名称"] = row.ProductName;
                    drow["产品id"] = row.ProductId;
                    drow["景区名称"] = row.SightName;
                    drow["销售价格"] = row.PriceInfo.SellPrice;
                    drow["购买人数"] = 1;
                    dataTable.Rows.Add(drow);
                }
            }
            Invoke(new InvokeHandler(delegate ()
            {
                dataGridView1.DataSource = dataTable;
            }));
            SetMsg("获取订单： \r\n" + JsonHelper.ObjectToJson(result));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(CancelOrder)
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void CancelOrder()
        {
            OrderCancelRequest request = new OrderCancelRequest
            {
                Body = new OrderCancelBody
                {
                    OrderInfo = new FengjingSDK461.Model.Request.OrderCancelInfo
                    {
                        OrderId = textBox3.Text,
                        OrderPrice = Convert.ToDecimal(textBox9.Text),
                        OrderQuantity = Convert.ToInt32(textBox10.Text),
                        reason = textBox11.Text,
                        Seq = textBox12.Text
                    }
                }
            };
            var result = Class1.CancelOrder(request);
            SetMsg("取消订单： \r\n" + JsonHelper.ObjectToJson(result));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //创建订单
            Thread thread = new Thread(CreateOrder)
            {
                IsBackground = true
            };
            thread.Start();
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        private void CreateOrder()
        {
            OrderCreateRequest request = new OrderCreateRequest
            {
                Body = new OrderCreateBody
                {
                    OrderInfo = new OrderInfo
                    {
                        OrderOtaId = textBox4.Text,
                        OrderPayStatus = 1,
                        OrderPrice = 20,
                        OrderQuantity = 1,
                        TicketList = new List<ProductItem>(),
                        VisitDate = textTime.Text,
                        ContactPerson = new ContactPerson
                        {
                            BuyName = textBox5.Text,
                            Name = textBox6.Text,
                            Mobile = textBox7.Text,
                            CardType = comboBox2.Text,
                            CardNo = textBox8.Text

                        }
                    }
                }
            };
            List<ProductItem> selectItem = new List<ProductItem>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == true)
                {
                    selectItem.Add(new ProductItem
                    {
                        ProductId = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value),
                        ProductName = dataGridView1.Rows[i].Cells[0].Value.ToString(),
                        SellPrice = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value),
                        Quantity = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value)
                    });

                    var dd = dataGridView1.Rows[i].Cells[1].Value.ToString();
                }
            }
            if (selectItem.Count <= 0)
            {
                MessageBox.Show("请选择你要购买的产品");
                return;
            }

            request.Body.OrderInfo.TicketList = selectItem;
            request.Body.OrderInfo.OrderPrice = selectItem.Sum(a => a.SellPrice * a.Quantity);
            request.Body.OrderInfo.OrderQuantity = selectItem.Sum(a => a.Quantity);

            var result = Class1.CreateOrder(request);
            if (result.Body != null)
            {
                textBox3.Text = result.Body.OrderId;
                textBox9.Text = request.Body.OrderInfo.OrderPrice.ToString();
                textBox10.Text = request.Body.OrderInfo.OrderQuantity.ToString();

                SetMsg("创建订单： 订单号 " + result.Body.OrderId + "\r\n");
            }
            SetMsg("创建订单： \r\n" + JsonHelper.ObjectToJson(result));
            textBox4.Text = OrderHelper.GenerateOrderNo();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //创建订单
        }

        public void SetMsg(string msg)
        {
            textBox2.Text = DateTime.Now + "  " + msg + "\r\n" + textBox2.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //查询订单
            Thread thread = new Thread(QueryOrder)
            {
                IsBackground = true
            };
            thread.Start();
        }
        private void QueryOrder()
        {
            var queryOrderId = textQueryOrder.Text;
            if (string.IsNullOrEmpty(queryOrderId))
            {
                MessageBox.Show("请填写订单号");
                return;
            }
            var result = Class1.QueryOrder(new OrderQuery
            {
                OrderId = queryOrderId
            });
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("产品名称");
            dataTable.Columns.Add("产品id");
            dataTable.Columns.Add("销售价格");
            dataTable.Columns.Add("状态");
            dataTable.Columns.Add("购买人数");
            dataTable.Columns.Add("已消费的票数");
            dataTable.Columns.Add("电子凭证号");
            dataTable.Rows.Clear();

            if (result.Body != null)
            {
                var order = result.Body.OrderInfo;
                textResultMsg.Text = "订单号：" + order.OrderId + "\r\n\r\n";
                textResultMsg.Text += "订单金额：" + order.OrderPrice + "\r\n\r\n";
                textResultMsg.Text += "订单总票数：" + order.OrderQuantity + "\r\n\r\n";
                textResultMsg.Text += "姓名：" + order.ContactPerson.Name + "\r\n\r\n";
                textResultMsg.Text += "手机号码：" + order.ContactPerson.Mobile + "\r\n\r\n";
                textResultMsg.Text += "证件类型：" + order.ContactPerson.CardType + "\r\n\r\n";
                textResultMsg.Text += "证件号码：" + order.ContactPerson.CardNo + "\r\n\r\n";
                foreach (var row in order.EticketInfo)
                {
                    DataRow drow = dataTable.NewRow();
                    drow["产品名称"] = row.ProductName;
                    drow["产品id"] = row.ProductId;
                    drow["销售价格"] = row.SellPrice;
                    drow["状态"] = row.Status;
                    drow["购买人数"] = row.EticketQuantity;
                    drow["已消费的票数"] = row.UseQuantity;
                    drow["电子凭证号"] = row.EticektNo;
                    dataTable.Rows.Add(drow);
                }
            }
            Invoke(new InvokeHandler(delegate ()
            {
                dataGridView2.DataSource = dataTable;
            }));
            SetMsg("查询订单： \r\n" + JsonHelper.ObjectToJson(result));
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //修改订单
            Thread thread = new Thread(UpdateOrder)
            {
                IsBackground = true
            };
            thread.Start();
        }
        private void UpdateOrder()
        {
            var result = Class1.UpdateOrder(new OrderUpdateBody
            {
                OrderInfo = new OrderUpdateInfo
                {
                    OrderId = textBox20.Text,
                    VisitDate = textBox15.Text,
                    ContactPerson = new OrderUpdateContactPerson
                    {
                        BuyName = textBox19.Text,
                        Name = textBox18.Text,
                        Mobile = textBox17.Text,
                        CardType = comboBox3.Text,
                        CardNo = textBox16.Text
                    }
                }
            });
            SetMsg("修改订单： \r\n" + JsonHelper.ObjectToJson(result));
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //发送入园凭证
            Thread thread = new Thread(SendMessage)
            {
                IsBackground = true
            };
            thread.Start();
        }
        private void SendMessage()
        {
            var result = Class1.SendMessage(new MessageSendBody
            {
                OrderInfo = new MessageSendOrderInfo
                {
                    OrderId = textBox21.Text,
                    phoneNumber = textBox22.Text
                }
            });
            SetMsg("修改订单： \r\n" + JsonHelper.ObjectToJson(result));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //创建单个产品订单
            Thread thread = new Thread(CreateSingleOrder)
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //下单验证
            Thread thread = new Thread(VerifyOrder)
            {
                IsBackground = true
            };
            thread.Start();
        }

        /// <summary>
        /// 创建单个产品订单
        /// </summary>
        private void CreateSingleOrder()
        {
            OrderSingleCreateRequest request = new OrderSingleCreateRequest
            {
                Body = new OrderSingleCreateBody
                {
                    OrderInfo = new OrderSingleInfo
                    {
                        OrderOtaId = textBox4.Text,
                        OrderPayStatus = 1,
                        OrderPrice = 20,
                        OrderQuantity = 1,
                        Ticket = new ProductItem(),
                        VisitDate = textTime.Text,
                        ContactPerson = new ContactPerson
                        {
                            BuyName = textBox5.Text,
                            Name = textBox6.Text,
                            Mobile = textBox7.Text,
                            CardType = comboBox2.Text,
                            CardNo = textBox8.Text

                        }
                    }
                }
            };
            List<ProductItem> selectItem = new List<ProductItem>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == true)
                {
                    selectItem.Add(new ProductItem
                    {
                        ProductId = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value),
                        ProductName = dataGridView1.Rows[i].Cells[0].Value.ToString(),
                        SellPrice = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value),
                        Quantity = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value)
                    });

                    var dd = dataGridView1.Rows[i].Cells[1].Value.ToString();
                }
            }
            if (selectItem.Count <= 0)
            {
                MessageBox.Show("请选择你要购买的产品");
                return;
            }
            var ticketInfo = selectItem.FirstOrDefault();
            request.Body.OrderInfo.Ticket = ticketInfo;
            request.Body.OrderInfo.OrderPrice = ticketInfo.SellPrice * ticketInfo.Quantity;
            request.Body.OrderInfo.OrderQuantity = ticketInfo.Quantity;

            var result = Class1.SingleCreateOrder(request);
            if (result.Body != null)
            {
                textBox3.Text = result.Body.OrderId;
                textBox9.Text = request.Body.OrderInfo.OrderPrice.ToString();
                textBox10.Text = request.Body.OrderInfo.OrderQuantity.ToString();

                SetMsg("创建单个订单： 订单号 " + result.Body.OrderId + "\r\n");
            }
            SetMsg("创建单个订单： \r\n" + JsonHelper.ObjectToJson(result));
            textBox4.Text = OrderHelper.GenerateOrderNo();
        }

        /// <summary>
        /// 下单验证
        /// </summary>
        private void VerifyOrder()
        {
            OrderSingleCreateRequest request = new OrderSingleCreateRequest
            {
                Body = new OrderSingleCreateBody
                {
                    OrderInfo = new OrderSingleInfo
                    {
                        OrderOtaId = textBox4.Text,
                        OrderPayStatus = 1,
                        OrderPrice = 20,
                        OrderQuantity = 1,
                        Ticket = new ProductItem(),
                        VisitDate = textTime.Text,
                        ContactPerson = new ContactPerson
                        {
                            BuyName = textBox5.Text,
                            Name = textBox6.Text,
                            Mobile = textBox7.Text,
                            CardType = comboBox2.Text,
                            CardNo = textBox8.Text

                        }
                    }
                }
            };
            List<ProductItem> selectItem = new List<ProductItem>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == true)
                {
                    selectItem.Add(new ProductItem
                    {
                        ProductId = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value),
                        ProductName = dataGridView1.Rows[i].Cells[0].Value.ToString(),
                        SellPrice = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value),
                        Quantity = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value)
                    });

                    var dd = dataGridView1.Rows[i].Cells[1].Value.ToString();
                }
            }
            if (selectItem.Count <= 0)
            {
                MessageBox.Show("请选择你要购买的产品");
                return;
            }
            var ticketInfo = selectItem.FirstOrDefault();
            request.Body.OrderInfo.Ticket = ticketInfo;
            request.Body.OrderInfo.OrderPrice = ticketInfo.SellPrice * ticketInfo.Quantity;
            request.Body.OrderInfo.OrderQuantity = ticketInfo.Quantity;

            var result = Class1.VerifyOrder(request);
            if (result.Body != null)
            {
                textBox3.Text = result.Body.OrderId;
                textBox9.Text = request.Body.OrderInfo.OrderPrice.ToString();
                textBox10.Text = request.Body.OrderInfo.OrderQuantity.ToString();
            }
            SetMsg("下单验证接口： \r\n" + JsonHelper.ObjectToJson(result));
            textBox4.Text = OrderHelper.GenerateOrderNo();
        }
    }
}

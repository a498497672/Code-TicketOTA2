using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Utility.Helpers
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class CheckCodeHelper
    {

        #region 生成验证码

        /// <summary>
        /// 创建验证码图片
        /// </summary>
        public static byte[] CreateCheckCodeImage(string checkCode)
        {
            Bitmap image = new Bitmap(80, 30);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的背景噪音线
                for (int i = 0; i < 5; i++)
                {
                    int x1 = random.Next(image.Width + 10);
                    int x2 = random.Next(image.Width + 10);
                    int y1 = random.Next(image.Height + 10);
                    int y2 = random.Next(image.Height + 10);
                    Pen p = new Pen(Color.Gray, 4);
                    g.DrawLine(p, x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 15, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                    Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 5, 5);
                //画图片的前景噪音点
                for (int i = 0; i < 200; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Gif);
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        /// <summary>
        /// 生成验证码code
        /// </summary>
        /// <returns></returns>
        public static string GenerateCheckCode()
        {
            string checkCode = String.Empty;
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                var number = random.Next();
                char code;
                if (number % 2 == 0)
                {
                    code = (char)('0' + (char)(number % 10));
                }
                else
                {
                    code = (char)('A' + (char)(number % 26));
                }
                checkCode += code.ToString();
            }
            return checkCode;
        }


        #endregion
    }
}

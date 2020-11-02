using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgDemo
{
    public partial class Form1 : Form
    {
        private PointF point;
        private Bitmap pbitmap;
        private Color pointColor = Color.LavenderBlush;
        private Color seletedColor = Color.Transparent;

        public Form1()
        {
            InitializeComponent();
            textBox1.LostFocus += TextLostFocus;
        }

        private void TextLostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Name.Equals("tbEndFloor"))
            {
                if (!int.TryParse(tb.Text.Trim(), out int temp))
                {
                    tb.Text = "100";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            pbitmap = new Bitmap(img);
            pbitmap = Conver_3(pbitmap, pbitmap.Width, pbitmap.Height, pointColor.R, pointColor.G, pointColor.B, seletedColor.R, seletedColor.G, seletedColor.B);
            //pbitmap = Conver_2(pbitmap, pbitmap.Width, pbitmap.Height, pointColor.R, pointColor.G, pointColor.B);
            pictureBox1.Image = pbitmap;
            button5.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image img = this.pictureBox1.Image;//.Image;
            using (Bitmap bmp = new Bitmap(img))
            {
                if (this.point.X >= bmp.Width)
                {
                    this.point.X = bmp.Width - 1;
                }
                if (this.point.Y >= bmp.Height)
                {
                    this.point.Y = bmp.Height - 1;
                }
                pointColor = bmp.GetPixel((int)this.point.X, (int)this.point.Y);
                pictureBox1.BackColor = pointColor;
            }
        }
        #region 图片指定颜色替换成另一种颜色 Conver_3(Bitmap img, int w, int h, int R, int G, int B, int r, int g, int b)
        /// <summary>
        /// 指定颜色替换成另一种颜色
        /// </summary>
        /// <param name="img">原图</param>
        /// <param name="w">图宽</param>
        /// <param name="h">图高</param>
        /// <param name="R">要被替换颜色的RGB的R</param>
        /// <param name="G">要被替换颜色的RGB的G</param>
        /// <param name="B">要被替换颜色的RGB的B</param>
        /// <param name="r">替换色的RGB的R</param>
        /// <param name="g">替换色的RGB的G</param>
        /// <param name="b">替换色的RGB的B</param>
        /// <returns>处理后的结果图像</returns>
        public Bitmap Conver_3(Bitmap img, int w, int h, int R, int G, int B, int r, int g, int b)
        {
            Bitmap bt = new Bitmap(ConvertTo32bpp(img));
            Rectangle rect = new Rectangle(0, 0, w, h);
            BitmapData bmpdata = bt.LockBits(rect, ImageLockMode.ReadWrite, bt.PixelFormat);
            IntPtr ptr = bmpdata.Scan0;
            int bytes = Math.Abs(bmpdata.Stride) * bt.Height;
            byte[] rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            int len = rgbValues.Length;
            byte a = (byte)int.Parse(textBox1.Text);
            byte R1 = (byte)R;
            byte G1 = (byte)G;
            byte B1 = (byte)B;
            byte r1 = (byte)r;
            byte g1 = (byte)g;
            byte b1 = (byte)b;
            for (int i = 0; i < len; i += 4)
            {
                //Format32bppRgb是用4个字节表示一个像素,第一个字节表示RGB的B值,第一个表示为G值,第三个表示为R值,第四个表示为Alpha值
                if (Math.Abs(rgbValues[i] - B1) < a && Math.Abs(rgbValues[i + 1] - G1) < a && Math.Abs(rgbValues[i + 2] - R1) < a)
                {
                    rgbValues[i] = b1;
                    rgbValues[i + 1] = g1;
                    rgbValues[i + 2] = r1;
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bt.UnlockBits(bmpdata);
            return bt;
        }
        #endregion      
        #region 图片背景透明化 Conver_1(Bitmap img, int w, int h)
        public Bitmap Conver_1(Bitmap img, int w, int h)
        {
            Bitmap bt = new Bitmap(ConvertTo32bpp(img));
            Rectangle rect = new Rectangle(0, 0, bt.Width, bt.Height);
            BitmapData bmpdata = bt.LockBits(rect, ImageLockMode.ReadWrite, bt.PixelFormat);
            IntPtr ptr = bmpdata.Scan0;
            int bytes = Math.Abs(bmpdata.Stride) * bt.Height;
            byte[] rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            int len = rgbValues.Length;
            byte a = (byte)40;//颜色的误差范围
            for (int i = 0; i < len; i += 4)
            {
                //Format32bppRgb是用4个字节表示一个像素,第一个字节表示RGB的B值,第一个表示为G值,第三个表示为R值,第四个表示为Alpha值
                if (Math.Abs(rgbValues[i] - rgbValues[0]) < a && Math.Abs(rgbValues[i + 1] - rgbValues[1]) < a && Math.Abs(rgbValues[i + 2] - rgbValues[2]) < a)
                {
                    rgbValues[i + 3] = (byte)0;
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bt.UnlockBits(bmpdata);
            return bt;
        }
        #endregion

        #region 图片指定颜色透明化 Conver_2(Bitmap img, int w, int h, int R, int G, int B)
        /// <summary>
        /// 指定颜色透明化
        /// </summary>
        /// <param name="img">原图</param>
        /// <param name="w">原图宽度</param>
        /// <param name="h">原图高度</param>
        /// <param name="R">指定颜色RGB的R值</param>
        /// <param name="G">指定颜色RGB的G值</param>
        /// <param name="B">指定颜色RGB的B值</param>
        /// <returns>处理后的结果图像</returns>
        public Bitmap Conver_2(Bitmap img, int w, int h, int R, int G, int B)
        {

            //测试结果不成功,图片没有变化
            Bitmap bt = new Bitmap(ConvertTo32bpp(img));
            Rectangle rect = new Rectangle(0, 0, w, h);
            BitmapData bmpdata = bt.LockBits(rect, ImageLockMode.ReadWrite, bt.PixelFormat);
            IntPtr ptr = bmpdata.Scan0;
            int bytes = Math.Abs(bmpdata.Stride) * bt.Height;
            byte[] rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            int len = rgbValues.Length;
            byte a = 40;
            byte R1 = (byte)R;
            byte G1 = (byte)G;
            byte B1 = (byte)B;
            for (int i = 0; i < len; i += 4)
            {
                //Format32bppRgb是用4个字节表示一个像素,第一个字节表示RGB的B值,第二个表示为G值,第三个表示为R值,第四个表示为Alpha值
                if (Math.Abs(rgbValues[i] - B1) < a && Math.Abs(rgbValues[i + 1] - G1) < a && Math.Abs(rgbValues[i + 2] - R1) < a)
                {
                    rgbValues[i + 3] = (byte)0;
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bt.UnlockBits(bmpdata);
            return bt;
        }
        #endregion

        public Bitmap ConvertTo32bpp(Image img)
        {
            var bmp = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
            return bmp;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int originalWidth = this.pictureBox1.Image.Width;
            int originalHeight = this.pictureBox1.Image.Height;

            PropertyInfo rectangleProperty = this.pictureBox1.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
            Rectangle rectangle = (Rectangle)rectangleProperty.GetValue(this.pictureBox1, null);

            int currentWidth = rectangle.Width;
            int currentHeight = rectangle.Height;

            double rate = (double)currentHeight / (double)originalHeight;

            int black_left_width = (currentWidth == this.pictureBox1.Width) ? 0 : (this.pictureBox1.Width - currentWidth) / 2;
            int black_top_height = (currentHeight == this.pictureBox1.Height) ? 0 : (this.pictureBox1.Height - currentHeight) / 2;

            int zoom_x = e.X - black_left_width;
            int zoom_y = e.Y - black_top_height;

            double original_x = (double)zoom_x / rate;
            double original_y = (double)zoom_y / rate;

            this.point.X = (float)original_x;
            this.point.Y = (float)original_y;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                seletedColor = color.Color;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(fileDialog.FileName);
                pictureBox1.Width = pictureBox1.Image.Width;
                pictureBox1.Height = pictureBox1.Image.Height;
                pictureBox1.Dock = DockStyle.Fill;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            string strSavePath = "";
            //按下确定选择的按钮  
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                //记录选中的目录 
                strSavePath = folderDialog.SelectedPath + "\\";
            }
            else
            {
                button5.Enabled = false;
                return;
            }
            string filename = strSavePath + DateTime.Now.Ticks + ".jpg";
            CompressImage(pbitmap, filename);
            //pbitmap.Save(filename);
        }

        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片地址</param>
        /// <param name="dFile">压缩后保存图片地址</param>
        /// <param name="flag">压缩质量（数字越小压缩率越高）1-100</param>
        /// <param name="size">压缩后图片的最大大小</param>
        /// <param name="sfsc">是否是第一次调用</param>
        /// <returns></returns>
        public bool CompressImage(Image sFile, string dFile, int flag = 90, int size = 300, bool sfsc = true)
        {
            Image iSource = sFile;
            ImageFormat tFormat = iSource.RawFormat;
            int dHeight = iSource.Height / 2;
            int dWidth = iSource.Width / 2;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Width * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);
            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;

            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
                    FileInfo fi = new FileInfo(dFile);
                    if (fi.Length > 1024 * size)
                    {
                        flag = flag - 10;
                        CompressImage(sFile, dFile, flag, size, false);
                    }
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }
        }
    }


    internal class MouseKeyHook
    {
        internal delegate void MouseMoveHandler(object sender, MouseEventArgs e);
        internal event MouseMoveHandler MouseMoveEvent;

        internal delegate void MouseClickHandler(object sender, MouseEventArgs e);
        internal event MouseClickHandler MouseClickEvent;

        internal event KeyEventHandler DirectionKeyDown;

        private WinAPI.boardProc MoveBoardHookProcedure;
        private WinAPI.boardProc KeyBoardHookProcedure;

        private int keyHookStatus = 0;
        private int moveHookStatus = 0;

        /// <summary>
        /// 安装钩子
        /// </summary>
        internal void Hook_Start()
        {
            if (this.keyHookStatus == 0)
            {
                this.KeyBoardHookProcedure = new WinAPI.boardProc(this.KeyHookProc);
                this.keyHookStatus = WinAPI.SetWindowsHookEx(WinAPI.WH_KEYBOARD_LL, this.KeyBoardHookProcedure, WinAPI.GetModuleHandle(Process.GetCurrentProcess().MainModule.ModuleName), 0);
            }
            if (this.moveHookStatus == 0)
            {
                this.MoveBoardHookProcedure = new WinAPI.boardProc(this.MouseHookProc);
                this.moveHookStatus = WinAPI.SetWindowsHookEx(WinAPI.WH_MOUSE_LL, this.MoveBoardHookProcedure, IntPtr.Zero, 0);
            }
        }

        /// <summary>
        /// 取消钩子
        /// </summary>
        internal void Hook_Clear()
        {
            if (this.keyHookStatus != 0)
            {
                WinAPI.UnhookWindowsHookEx(this.keyHookStatus);
                this.keyHookStatus = 0;
            }
            if (this.moveHookStatus != 0)
            {
                WinAPI.UnhookWindowsHookEx(this.moveHookStatus);
                this.moveHookStatus = 0;
            }
        }

        /// <summary>
        /// 鼠标处理事件
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private int MouseHookProc(int nCode, int wParam, IntPtr lParam)
        {
            WinAPI.MouseHookStruct MyMouseHookStruct = (WinAPI.MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(WinAPI.MouseHookStruct));
            switch (wParam)
            {
                case WinAPI.WM_MOUSEMOVE:
                    {
                        this.MouseMoveEvent(this, new MouseEventArgs(MouseButtons.None, 0, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, 0));
                        break;
                    }
                case WinAPI.WM_RBUTTONDOWN:
                    {
                        var e = new MouseEventArgs(MouseButtons.Right, 1, MyMouseHookStruct.pt.X, MyMouseHookStruct.pt.Y, 0);
                        this.MouseClickEvent(this, e);
                        return 1;
                    }

            }
            return WinAPI.CallNextHookEx(this.moveHookStatus, nCode, wParam, lParam);
        }

        /// <summary>
        /// 键盘处理事件
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private int KeyHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                WinAPI.KeyHookStruct kbh = (WinAPI.KeyHookStruct)Marshal.PtrToStructure(lParam, typeof(WinAPI.KeyHookStruct));//获取钩子的相关信息
                KeyEventArgs e = new KeyEventArgs((Keys)(kbh.vkCode));//获取KeyEventArgs事件的相磁信息
                switch (e.KeyCode)
                {
                    case Keys.RButton | Keys.MButton | Keys.Space:
                    case Keys.Back | Keys.Space:
                    case Keys.LButton | Keys.MButton | Keys.Space:
                    case Keys.LButton | Keys.RButton | Keys.MButton | Keys.Space:
                        {
                            if (kbh.flags == 1)
                            {
                                // 这里写按下后做什么事 
                                this.DirectionKeyDown(this, e);
                            }
                            return 1;
                        }
                }
            }
            return WinAPI.CallNextHookEx(this.keyHookStatus, nCode, wParam, lParam);
        }

    }


    public static class WinAPI
    {
        internal delegate int boardProc(int nCode, int wParam, IntPtr lParam);//钩子拦截处理函数

        internal const int WH_KEYBOARD_LL = 13;//键盘事件

        internal const int SW_SHOWNOACTIVATE = 4;//显示和激活窗体
        internal const int SW_HIDE = 0;//应灿窗体

        internal const int WH_MOUSE_LL = 14;//鼠标事件
        internal const int WM_MOUSEMOVE = 0x200;
        internal const int WM_LBUTTONDOWN = 0x201;
        internal const int WM_RBUTTONDOWN = 0x204;
        internal const int WM_MBUTTONDOWN = 0x207;
        internal const int WM_LBUTTONUP = 0x202;
        internal const int WM_RBUTTONUP = 0x205;
        internal const int WM_MBUTTONUP = 0x208;
        internal const int WM_LBUTTONDBLCLK = 0x203;
        internal const int WM_RBUTTONDBLCLK = 0x206;
        internal const int WM_MBUTTONDBLCLK = 0x209;

        /// <summary>
        /// 设置窗体置顶
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 查找窗体
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 获取窗体Rectangle
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern int GetWindowRect(IntPtr hwnd, out RectangleStruct lpRect);

        /// <summary>
        /// 设置窗体状态
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int ShowWindow(IntPtr hWnd, uint nCmdShow);

        /// <summary>
        /// 获取光标位置
        /// </summary>
        /// <param name="pot"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        internal extern static bool GetCursorPos(ref PointStruct pot);

        /// <summary>
        /// 设置光标位置
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        [DllImport("User32.dll")]
        internal extern static void SetCursorPos(int x, int y);

        /// <summary>
        /// 安装钩子
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="lpfn"></param>
        /// <param name="hInstance"></param>
        /// <param name="threadId"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern int SetWindowsHookEx(int idHook, boardProc lpfn, IntPtr hInstance, int threadId);

        /// <summary>
        /// 卸载钩子
        /// </summary>
        /// <param name="idHook"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool UnhookWindowsHookEx(int idHook);

        /// <summary>
        /// 调用下一个钩子
        /// </summary>
        /// <param name="idHook"></param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        /// <summary>
        /// 获取模块句柄
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetModuleHandle(string name);

        /// <summary>
        /// 矩形坐标
        /// </summary>
        internal struct RectangleStruct
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        /// <summary>
        /// 坐标
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct PointStruct
        {
            public int X;
            public int Y;
        }

        /// <summary>
        /// 鼠标钩子返回信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal class MouseHookStruct
        {
            public PointStruct pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        /// <summary>
        /// 键盘钩子返回信息结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal class KeyHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgDemo
{
    public partial class Form1 : Form
    {
        private Point point;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            Bitmap pbitmap = new Bitmap(img);
            pbitmap = Conver_3(pbitmap, pbitmap.Width, pbitmap.Height, pointColor.R, pointColor.G, pointColor.B, seletedColor.R, seletedColor.G, seletedColor.B);

            pictureBox1.Image = pbitmap;
        }

        Color pointColor = Color.LavenderBlush;
        Color seletedColor = Color.LavenderBlush;

        private void button2_Click(object sender, EventArgs e)
        {
            Image img = this.pictureBox1.Image;
            using (Bitmap bmp = new Bitmap(img))
            {
                pointColor = bmp.GetPixel(this.point.X, this.point.Y);
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
        public static Bitmap Conver_3(Bitmap img, int w, int h, int R, int G, int B, int r, int g, int b)
        {
            Bitmap bt = new Bitmap(ConvertTo32bpp(img));
            Rectangle rect = new Rectangle(0, 0, w, h);
            BitmapData bmpdata = bt.LockBits(rect, ImageLockMode.ReadWrite, bt.PixelFormat);
            IntPtr ptr = bmpdata.Scan0;
            int bytes = Math.Abs(bmpdata.Stride) * bt.Height;
            byte[] rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            int len = rgbValues.Length;
            byte a =100;
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
        public static Bitmap Conver_1(Bitmap img, int w, int h)
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
        public static Bitmap Conver_2(Bitmap img, int w, int h, int R, int G, int B)
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

        public static Bitmap ConvertTo32bpp(Image img)
        {
            var bmp = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
            return bmp;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.point.X = e.X;
            this.point.Y = e.Y;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                seletedColor = color.Color;
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

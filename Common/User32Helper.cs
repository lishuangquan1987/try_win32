using System.Runtime.InteropServices;
using System.Text;

namespace Common
{
    public class User32Helper
    {
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll")]
        public static extern long GetWindowLong(IntPtr hWnd, int nlndex);

        /// <summary>
        /// 获取当前活动窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        #region 获取窗口信息 https://www.cnblogs.com/mayishangtaijie/p/18033119
        //获取窗口标题
        [DllImport("user32", SetLastError = true)]
        public static extern int GetWindowText(
            IntPtr hWnd,//窗口句柄
            StringBuilder lpString,//标题
            int nMaxCount //最大值
            );

        //获取类的名字
        [DllImport("user32.dll")]
        public static extern int GetClassName(
            IntPtr hWnd,//句柄
            StringBuilder lpString, //类名
            int nMaxCount //最大值
            );


        public struct COPYDATASTRUCT
        {
            public IntPtr dwData; //可以是任意值
            public int cbData;    //指定lpData内存区域的字节数
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData; //发送给目录窗口所在进程的数据

        }
        /// <summary>
        /// 向指定句柄发送消息
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);


        public delegate bool EnumDesktopWindowsDelegate(IntPtr hWnd, uint lParam);
        /// <summary>
        /// https://www.cnblogs.com/Rolends/archive/2012/04/19/2456907.html
        /// 遍历所有桌面窗口
        /// </summary>
        /// <param name="hDesktop"></param>
        /// <param name="lpEnumCallbackFunction"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDesktopWindowsDelegate lpEnumCallbackFunction, IntPtr lParam);

        /// <summary>
        /// 获取窗口的子窗口
        /// </summary>
        /// <param name="parentHandle"></param>
        /// <param name="callback"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr parentHandle, EnumChildWindowsDelegate callback, IntPtr lParam);
        public delegate bool EnumChildWindowsDelegate(IntPtr hwnd, IntPtr lParam);
        #endregion
    }
}

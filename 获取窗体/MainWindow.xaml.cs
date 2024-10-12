using Common;
using Microsoft.Windows.Themes;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 获取窗体
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int WS_EX_TOOLWINDOW = 0x00000080;
        const int WS_EX_NOACTIVATE = 0x08000000;
        const int GWL_EXSTYLE = -20;
        const int WM_SETTEXT = 0x0C;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);

            int GWL_EXSTYLE = -20;

            var style = User32Helper.GetWindowLong(helper.Handle, GWL_EXSTYLE);

            //cp.ExStyle |= (WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW);
            //cp.Parent = IntPtr.Zero; // Keep this line only if you used UserControl

            style |= (WS_EX_NOACTIVATE | WS_EX_TOOLWINDOW);

            User32Helper.SetWindowLong(helper.Handle, GWL_EXSTYLE, new IntPtr(style));
        }
        //获取焦点窗体
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var handle = User32Helper.GetForegroundWindow();

            var (title, className) = GetWindowTitleAndClassName(handle);

            System.Windows.MessageBox.Show(title + "\n" + className + "\n" + handle);
        }

        //遍历窗口
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.listBox.Items.Clear();
            List<IntPtr> handles = new List<IntPtr>();
            User32Helper.EnumDesktopWindows(IntPtr.Zero, (handle, param) =>
            {
                handles.Add(handle);
                return true;
            }, IntPtr.Zero);

            foreach (var handle in handles)
            {
                var (title, className) = GetWindowTitleAndClassName(handle);
                this.listBox.Items.Add(title + "【" + className + "】" + "-----" + handle);
            }
        }

        public (string, string) GetWindowTitleAndClassName(IntPtr handle)
        {
            int length = 512;
            StringBuilder stringBuilder = new StringBuilder(length);
            User32Helper.GetWindowText(handle, stringBuilder, length);
            string title = stringBuilder.ToString();

            User32Helper.GetClassName(handle, stringBuilder, length);
            string className = stringBuilder.ToString();

            return (title, className);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var handle = User32Helper.GetForegroundWindow();


                string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                #region 不起作用
                //User32Helper.COPYDATASTRUCT cds;
                //cds.dwData= (IntPtr)0;
                //cds.cbData = text.Length + 1;
                //cds.lpData = text;

                //var result= User32Helper.SendMessage(handle, WM_SETTEXT, IntPtr.Zero, ref cds);


                //Marshal.FreeHGlobal(buffer);
                #endregion


                SendKeys.SendWait(text);
            }
            catch (Exception ee)
            {
                System.Windows.MessageBox.Show(ee.Message);
            }
        }
    }
}
using Common;
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

namespace 圆形窗体
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int WS_EX_TOOLWINDOW = 0x00000080;
        const int WS_EX_NOACTIVATE = 0x08000000;
        const int GWL_EXSTYLE = -20;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded+= MainWindow_Loaded;
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

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton ==  MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
using Biba.UIComponent;
using Biba.Util;
using System.Windows;

namespace Biba
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Ini User;
        public static Ini Data;
        public MainWindow()
        {
            InitializeComponent();
            User = new Ini("user.ini");
            Data = new Ini("data.ini");
        }

        private void Register(object sender, RoutedEventArgs e) => new RegisterDialog().ShowDialog();

        private void Login(object sender, RoutedEventArgs e)
        {
            string name = username.Text;
            string pwd = password.Password;
            if (string.IsNullOrEmpty(name))
                error.Text = "请输入用户名！";
            else if (string.IsNullOrEmpty(pwd))
                error.Text = "请输入密码！";
            else if (!ConfigUtil.ObjectExists(User, name))
                error.Text = "用户不存在，请注册！";
            else
            {
                var user = ConfigUtil.ReadSubject(User, name);
                if (!pwd.Equals(user.Password))
                    error.Text = "密码错误！";
                else
                    new DataDialog(user).ShowDialog();
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            User.Save();
            Data.Save();
        }
    }
}

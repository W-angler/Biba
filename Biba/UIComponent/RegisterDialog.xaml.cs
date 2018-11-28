using Biba.Model;
using Biba.Util;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Biba.UIComponent
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterDialog : Window
    {
        public RegisterDialog()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string name = username.Text;
            string pwd = password.Password;
            string confirmPassword = confirm.Password;
            string label = level.Text;
            if (string.IsNullOrEmpty(name))
                error.Text = "请输入用户名！";
            else if (string.IsNullOrEmpty(pwd))
                error.Text = "请输入密码！";
            else if (!pwd.Equals(confirmPassword))
                error.Text = "请确认密码一致！";
            else if (string.IsNullOrEmpty(label))
                error.Text = "请输入等级！";
            else if (ConfigUtil.SubjectExists(MainWindow.User, name))
                error.Text = "用户已存在，请换个用户名！";
            else
            {
                ConfigUtil.WriteSubject(MainWindow.User, new BibaSubject()
                {
                    Name = name,
                    Password = confirmPassword,
                    Level = int.Parse(label)
                });
                Close();
            }
        }
        /// <summary>
        /// 只能输入数字
        /// </summary>
        private static readonly Regex regex = new Regex("[^0-9.-]+");
        private void Level_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

using Biba.Model;
using Biba.Util;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Biba.UIComponent
{
    /// <summary>
    /// NewDialog.xaml 的交互逻辑
    /// </summary>
    public partial class NewDialog : Window
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int Level { get; set; }

        private BibaSubject subject;

        public NewDialog(BibaSubject subject)
        {
            InitializeComponent();
            this.subject = subject;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string inputKey = key.Text;
            string inputValue = value.Text;
            string inputLevel = level.Text;
            if (string.IsNullOrEmpty(inputKey))
                error.Text = "请输入键！";
            else if (string.IsNullOrEmpty(inputValue))
                error.Text = "请输入值！";
            else if (string.IsNullOrEmpty(inputLevel))
                error.Text = "请输入等级！";
            else if (ConfigUtil.ObjectExists(MainWindow.Data, inputKey))
                error.Text = "键已存在，请换个！";
            else if (!subject.CanWrite(int.Parse(inputLevel)))
                error.Text = string.Format("无权限写等级为{0}的客体", inputLevel);
            else
            {
                Key = inputKey;
                Value = inputValue;
                Level = int.Parse(inputLevel);
                DialogResult = true;
                Close();
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
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

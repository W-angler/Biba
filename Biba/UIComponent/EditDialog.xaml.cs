using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biba.UIComponent
{
    /// <summary>
    /// UpdateDialog.xaml 的交互逻辑
    /// </summary>
    public partial class EditDialog : Window
    {
        public string Value { get; set; }
        public EditDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string inputValue = value.Text;
             if (string.IsNullOrEmpty(inputValue))
                error.Text = "请输入值！";
            else
            {
                Value = inputValue;
                DialogResult = true;
                Close();
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

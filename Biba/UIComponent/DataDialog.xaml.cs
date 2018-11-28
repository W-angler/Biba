using Biba.Model;
using Biba.Util;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Biba.UIComponent
{
    /// <summary>
    /// DataDialog.xaml 的交互逻辑
    /// </summary>
    public partial class DataDialog : Window
    {
        private BibaSubject Current;
        private readonly ObservableCollection<BibaObject> objects = new ObservableCollection<BibaObject>();
        public DataDialog(BibaSubject Current)
        {
            InitializeComponent();
            Title = string.Format("欢迎{0},您的权限为{1}", Current.Name, Current.Level);
            this.Current = Current;
            datas.DataContext = objects;
            foreach (var section in MainWindow.Data.GetSections())
            {
                var obj = ConfigUtil.ReadObject(MainWindow.Data, section);
                if (!Current.CanRead(obj))
                {
                    obj.Value = "无权限";
                }
                objects.Add(obj);
            }
        }

        private void Subjects_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        private void CommandBinding_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void CommandBinding_New(object sender, RoutedEventArgs e)
        {
            NewDialog newDialog = new NewDialog(Current);
            bool? dialogResult = newDialog.ShowDialog();
            switch (dialogResult)
            {
                case true:
                    var obj = new BibaObject()
                    {
                        Name = newDialog.Key,
                        Value = newDialog.Value,
                        Level = newDialog.Level
                    };
                    ConfigUtil.WriteObject(MainWindow.Data, obj);
                    if (!Current.CanRead(obj))
                    {
                        obj.Value = "无权限";
                    }
                    objects.Add(obj);
                    break;
                case false:
                    break;
                default:
                    break;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (!(datas.SelectedItem is BibaObject obj))
            {
                return;
            }
            if (!Current.CanWrite(obj))
            {
                MessageBox.Show("没有权限修改！");
                return;
            }
            EditDialog editDialog = new EditDialog();
            bool? dialogResult = editDialog.ShowDialog();
            switch (dialogResult)
            {
                case true:
                    obj.Value = editDialog.Value;
                    ConfigUtil.WriteObject(MainWindow.Data, obj);
                    break;
                case false:
                    break;
                default:
                    break;
            }
        }

        private void CommandBinding_Delete(object sender, RoutedEventArgs e)
        {
            if (!(datas.SelectedItem is BibaObject obj))
            {
                return;
            }
            if (!Current.CanWrite(obj))
            {
                MessageBox.Show("没有权限删除！");
                return;
            }
            objects.Remove(obj);
            ConfigUtil.DeleteObject(MainWindow.Data, obj);
            datas.Items.Refresh();
            MessageBox.Show("删除成功！");
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutControlView about = new AboutControlView();
            AboutControlViewModel vm = (AboutControlViewModel)about.FindResource("ViewModel");
            vm.ApplicationLogo= new BitmapImage(new System.Uri("pack://application:,,,/Biba.bmp"));
            vm.IsSemanticVersioning = true;
            vm.HyperlinkText = "https://github.com/w-angler/Biba";
            vm.Description = "Biba模型实现";
            vm.Copyright = "汪鹏程";
            vm.AdditionalNotes = "信息系统安全作业";
            vm.Window.Content = about;
            vm.Window.Show();
        }
    }

}

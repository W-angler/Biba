using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Biba.Model
{
    /// <summary>
    /// Biba客体
    /// </summary>
    public class BibaObject : INotifyPropertyChanged
    {
        private string value;
        public string Name { get; set; }
        public string Value
        {
            get { return value; }
            set
            {
                if (value.Equals(this.value)) return;
                this.value = value;
                OnPropertyChanged("Value");
            }
        }
        public int Level { get; set; }

        #region 属性更改
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion 属性更改
    }
}

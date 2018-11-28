namespace Biba.UIComponent
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media;

    public class AboutControlViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ImageSource _ApplicationLogo;
        private string _Title;
        private string _Description;
        private string _Version;
        private ImageSource _PublisherLogo;
        private string _Copyright;
        private string _AdditionalNotes;
        private string _HyperlinkText;
        private Uri _Hyperlink;
        private string _Publisher;
        private bool _isSemanticVersioning;

        #endregion

        #region Constructors

        public AboutControlViewModel()
        {
            Window = new Window();
            Window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Window.SizeToContent = SizeToContent.WidthAndHeight;
            Window.ResizeMode = ResizeMode.NoResize;
            Window.WindowStyle = WindowStyle.None;

            Window.ShowInTaskbar = false;
            Window.Title = "About";
            Window.Deactivated += Window_Deactivated;

            Assembly assembly = Assembly.GetEntryAssembly();
            Version = assembly.GetName().Version.ToString();
            Title = assembly.GetName().Name;

#if NET35 || NET40
			AssemblyCopyrightAttribute copyright = Attribute.GetCustomAttribute(assembly, typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute;
			AssemblyDescriptionAttribute description = Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute)) as AssemblyDescriptionAttribute;
			AssemblyCompanyAttribute company = Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute)) as AssemblyCompanyAttribute;
#else
            AssemblyCopyrightAttribute copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
            AssemblyDescriptionAttribute description = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
            AssemblyCompanyAttribute company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
#endif
            Copyright = copyright.Copyright;
            Description = description.Description;
            Publisher = company.Company;

            AdditionalNotes = "Further information about ...";
        }

        #endregion

        #region Properties

        public ImageSource ApplicationLogo
        {
            get { return _ApplicationLogo; }
            set
            {
                if (_ApplicationLogo != value)
                {
                    _ApplicationLogo = value;
                    OnPropertyChanged("ApplicationLogo");
                }
            }
        }

        public string Title
        {
            get { return _Title; }
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public bool IsSemanticVersioning
        {
            get { return _isSemanticVersioning; }
            set
            {
                _isSemanticVersioning = value;
                OnPropertyChanged("Version");
            }
        }

        public string Version
        {
            get
            {
                if (IsSemanticVersioning)
                {
                    var tmp = _Version.Split('.');
                    var version = string.Format("{0}.{1}.{2}", tmp[0], tmp[1], tmp[2]);
                    return version;
                }
                return _Version;
            }
            set
            {
                if (_Version != value)
                {
                    _Version = value;
                    OnPropertyChanged("Version");
                }
            }
        }
        public ImageSource PublisherLogo
        {
            get { return _PublisherLogo; }
            set
            {
                if (_PublisherLogo != value)
                {
                    _PublisherLogo = value;
                    OnPropertyChanged("PublisherLogo");
                }
            }
        }

        public string Publisher
        {
            get { return _Publisher; }
            set
            {
                if (_Publisher != value)
                {
                    _Publisher = value;
                    OnPropertyChanged("Publisher");
                }
            }
        }
        public string Copyright
        {
            get { return _Copyright; }
            set
            {
                if (_Copyright != value)
                {
                    _Copyright = value;
                    OnPropertyChanged("Copyright");
                }
            }
        }

        public string HyperlinkText
        {
            get { return _HyperlinkText; }
            set
            {
                try
                {
                    Hyperlink = new Uri(value);
                    _HyperlinkText = value;
                    OnPropertyChanged("HyperlinkText");
                }
                catch { }
            }
        }

        public Uri Hyperlink
        {
            get { return _Hyperlink; }
            set
            {
                if (_Hyperlink != value)
                {
                    _Hyperlink = value;
                    OnPropertyChanged("Hyperlink");
                }
            }
        }

        public string AdditionalNotes
        {
            get { return _AdditionalNotes; }
            set
            {
                if (_AdditionalNotes != value)
                {
                    _AdditionalNotes = value;
                    OnPropertyChanged("AdditionalNotes");
                }
            }
        }

        public Window Window { get; set; }

        #endregion

        void Window_Deactivated(object sender, System.EventArgs e) { Window.Close(); }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}

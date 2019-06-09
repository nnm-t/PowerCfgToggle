using System.ComponentModel;

namespace PowerCfgToggle
{
    using System.Windows;

    public partial class NotifyIcon : Component
    {
        public NotifyIcon()
        {
            InitializeComponent();

            this.toolStripMenuItemExitApp.Click += (sender, e) => { Application.Current.Shutdown(); };
        }

        public NotifyIcon(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}

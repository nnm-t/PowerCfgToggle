using System.ComponentModel;
using PowerCfgToggle.Properties;

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

        public void Popup(string title, string text)
        {
            myNotifyIcon.BalloonTipTitle = title;
            myNotifyIcon.BalloonTipText = text;
            
            myNotifyIcon.ShowBalloonTip(1000);
        }
    }
}

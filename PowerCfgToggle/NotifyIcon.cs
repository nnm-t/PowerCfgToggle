using System.ComponentModel;

namespace PowerCfgToggle
{
    using System.Windows;

    public partial class NotifyIcon : Component
    {
        public NotifyIcon()
        {
            InitializeComponent();

            toolStripMenuItemExitApp.Click += Shutdown;

            myNotifyIcon.BalloonTipClicked += Shutdown;
            myNotifyIcon.BalloonTipClosed += Shutdown;
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

        private void Shutdown<T>(object sender, T e)
        {
            Application.Current.Shutdown();
        }
    }
}

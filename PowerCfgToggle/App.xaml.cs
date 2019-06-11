using System;
using System.Threading.Tasks;
using System.Windows;

namespace PowerCfgToggle
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly NotifyIcon notifyIcon = new NotifyIcon();

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            
            var powerConfig = new PowerConfig();
            
            notifyIcon.Popup("電源設定", powerConfig.Execute());

            await Task.Delay(TimeSpan.FromSeconds(5));

            Current.Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            notifyIcon.Dispose();
        }
    }
}
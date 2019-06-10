namespace PowerCfgToggle
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private NotifyIcon notifyIcon = new NotifyIcon();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            
            var powerConfig = new PowerConfig();
            powerConfig.Execute();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            notifyIcon.Dispose();
        }
    }
}
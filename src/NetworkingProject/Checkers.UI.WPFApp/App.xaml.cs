using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Checkers.Domain.Interfaces;

namespace Checkers.UI.WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected async void ResetGame(object sender, EventArgs e)
        {
            await DependencyInjectionContainer.Get<ICheckersRepository>().ResetGameAsync();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Dispatcher.UnhandledException += ResetGame;
            AppDomain.CurrentDomain.UnhandledException += ResetGame;
            MainWindow mainWindow = new MainWindow(DependencyInjectionContainer.Get<ICheckersRepository>());
            mainWindow.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Dispatcher.UnhandledException -= ResetGame;
            AppDomain.CurrentDomain.UnhandledException -= ResetGame;
            ResetGame(null, null);
        }
    }
}

using Bender.DataAccess;
using Bender.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Bender.Utils;

namespace Bender
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (SingleInstance.AmIAlreadyRunning())
            {
                System.Windows.Application.Current.Shutdown();
                Environment.Exit(0);
            }
            base.OnStartup(e);
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            new BenderContext().Initialize();
            new MainWindow().Show();
        }
    }
}

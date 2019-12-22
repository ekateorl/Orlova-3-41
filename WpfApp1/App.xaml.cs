using DAL.Entities;
using DAL.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Util;
using WpfApp1.View;
using WpfApp1.ViewModel;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var kernel = new StandardKernel(new NinjectRegistrations());
            IDbRepository crudServ = kernel.Get<IDbRepository>();

            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            AuthInfo authInfo = new AuthInfo();
            var loginContext = new LoginWindowViewModel(crudServ, authInfo);
            var dialog = new LoginWindow(loginContext);
            var res = dialog.ShowDialog();
            if(res == true)
            {
                MainWindow window = new MainWindow(new ApplicationViewModel(crudServ, authInfo.User));
                window.ShowDialog();
            }
            Current.Shutdown(-1);
        }
    }
}

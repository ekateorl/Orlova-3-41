using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModel;
using WpfApp1.Util;
using DAL.Interfaces;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var kernel = new StandardKernel(new NinjectRegistrations());

            IDbRepository crudServ = kernel.Get<IDbRepository>();
            DataContext = new ApplicationViewModel(crudServ, MainFrame.NavigationService);
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}

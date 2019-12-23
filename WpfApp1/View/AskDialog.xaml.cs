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
using System.Windows.Shapes;
using WpfApp1.ViewModel;

namespace WpfApp1.View
{
    /// <summary>
    /// Логика взаимодействия для AskDialog.xaml
    /// </summary>
    public partial class AskDialog : Window
    {
        public AskDialog(DialogVM dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }
    }
}

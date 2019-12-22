using WpfApp1.ViewModel;

using DAL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WpfApp1.ViewModel
{
    public class ApplicationViewModel 
    {
        IDbRepository crud;
        public NavigationService nav { get; set; }
        User user;

        public Visibility ManagerButtonVisibility { get; set; }

        public ApplicationViewModel(IDbRepository crud, User user)
        {
          //  this.nav = nav;
            this.crud = crud;
            this.user = user;
            if (user.UserType.Name == "Менеджер")
                ManagerButtonVisibility = Visibility.Visible;
            else
                ManagerButtonVisibility = Visibility.Collapsed;
        }

        private ICommand _specialistListCommand;
 
        public ICommand SpecialistListCommand
        {
            get
            {
                if (_specialistListCommand == null)
                    _specialistListCommand = new RelayCommand(obj =>
                    {
                        nav.Navigate(new View.SpecialistList(new SpecialistPageViewModel(crud, nav)));
                    });
                return _specialistListCommand;
            }
            set { _specialistListCommand = value; }
        }

        private ICommand _categoryListCommand;

        public ICommand CategoryListCommand
        {
            get
            {
                if (_categoryListCommand == null)
                    _categoryListCommand = new RelayCommand(obj =>
                    {
                        nav.Navigate(new View.CategoryList(new CategoryListViewModel(crud, nav)));
                    });
                return _categoryListCommand;
            }
            set { _categoryListCommand = value; }
        }

        private ICommand _appPageCommand;

        public ICommand AppPageCommand
        {
            get
            {
                if (_appPageCommand == null)
                    _appPageCommand = new RelayCommand(obj =>
                    {
                        nav.Navigate(new View.AppointmentPage(new AppointmentPageVM(crud, nav)));
                    });
                return _appPageCommand;
            }
            set { _appPageCommand = value; }
        }
    }
}

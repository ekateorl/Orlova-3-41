using WpfApp1.ViewModel;
using DAL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Navigation;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    public class ServiceListViewModel
    {
        public string Name { get; set; }
        IDbRepository crud;
        NavigationService nav;
        public List<Service> Services { get; set; }

        public ServiceListViewModel(IDbRepository crud, NavigationService nav, Category category)
        {
            this.crud = crud;
            this.nav = nav;
            Services = crud.Services.GetList().Where(i=>i.CategoryFk == category.CategoryId && i.Provided).OrderBy(i => i.Name).ToList();
        }
        private ICommand _infoCommand;

        public ICommand InfoCommand
        {
            get
            {
                if (_infoCommand == null)
                    _infoCommand = new RelayCommand(obj =>
                    {
                        nav.Navigate(new View.ServiceInfo(new ServiceInfoViewModel(nav, crud, (Service)obj)));
                    });
                return _infoCommand;
            }
            set { _infoCommand = value; }
        }
    }
}
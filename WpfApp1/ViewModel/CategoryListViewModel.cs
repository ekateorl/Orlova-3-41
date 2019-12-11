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
    public class CategoryListViewModel
    {
        public string Name { get; set; }
        IDbRepository crud;
        NavigationService nav;
        public List<Category> Categories { get; set; }

        public CategoryListViewModel(IDbRepository crud, NavigationService nav)
        {
            this.crud = crud;
            this.nav = nav;
            Categories = crud.Categories.GetList().OrderBy(i => i.Name).ToList();
        }
        private ICommand _serviceCommand;

        public ICommand ServiceCommand
        {
            get
            {
                if (_serviceCommand == null)
                    _serviceCommand = new RelayCommand(obj =>
                    {
                        nav.Navigate(new View.ServiceList(new ServiceListViewModel( crud, nav, (Category)obj)));
                    });
                return _serviceCommand;
            }
            set { _serviceCommand = value; }
        }
    }
}
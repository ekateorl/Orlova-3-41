using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Entities;
using System.Windows.Navigation;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    class ServiceInfoViewModel
    {
        IDbRepository crud;
        NavigationService nav;
        public Service Service { get; set; }
        public List<User> Specialists { get; set; }
        
        public ServiceInfoViewModel(NavigationService nav, IDbRepository crud, Service service)
        {
            this.crud = crud;
            this.nav = nav;
            Service = service;
            Specialists = crud.ServiceSpecialists.GetList().Where(i => i.ServiceFk == Service.ServiceId).Select(i => i.User).ToList();
        }

        private ICommand _specialistCommand;

        public ICommand SpecialistCommand
        {
            get
            {
                if (_specialistCommand == null)
                    _specialistCommand = new RelayCommand(obj =>
                    {
                        nav.Navigate(new View.SpecialistInfo(new SpecialistInfoViewModel(nav, crud, (User)obj)));
                    });
                return _specialistCommand;
            }
            set { _specialistCommand = value; }
        }
    }
}

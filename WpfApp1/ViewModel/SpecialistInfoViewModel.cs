using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using DAL.Interfaces;
using DAL.Entities;
namespace WpfApp1.ViewModel
{
    public class SpecialistInfoViewModel
    {
        IDbRepository crud;
        NavigationService nav;
        public User Specialist { get; set; }
        public List<Service> Services { get; set; }
        public SpecialistInfoViewModel(NavigationService nav, IDbRepository crud, User specialist)
        {
            this.nav = nav;
            this.crud = crud;
            Specialist = specialist;
            Services = new List<Service>();
            List<ServiceSpecialist> serviceSpecialists = crud.ServiceSpecialists.GetList()
                .Where(i => i.SpecialistFk == Specialist.UserId).ToList();
            foreach (ServiceSpecialist s in serviceSpecialists)
                Services.Add(crud.Services.GetItem(s.ServiceFk));
        }

        private ICommand _serviceCommand;

        public ICommand ServiceCommand
        {
            get
            {
                if (_serviceCommand == null)
                    _serviceCommand = new RelayCommand(obj =>
                    {
                        nav.Navigate(new View.ServiceInfo(new ServiceInfoViewModel(nav,crud,(Service)obj)));
                    });
                return _serviceCommand;
            }
            set { _serviceCommand = value; }
        }
    }
}

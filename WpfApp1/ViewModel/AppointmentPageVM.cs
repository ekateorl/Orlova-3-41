using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using DAL.Entities;
using DAL.Interfaces;

namespace WpfApp1.ViewModel
{
    public class AppointmentPageVM : INotifyPropertyChanged
    {
        IDbRepository crud;

        NavigationService nav;

        public List<SpecialistAppVM> Specialists { get; set; }

        public DateTime Date { get; set; }

        public AppointmentPageVM(IDbRepository crud, NavigationService nav)
        {
            this.crud = crud;
            this.nav = nav;
            Specialists = new List<SpecialistAppVM>();
            List<User> users = crud.Users.GetList().Where(i => i.UserType.Name == "Мастер").OrderBy(i => i.Name).ToList();
            foreach (User u in users)
                Specialists.Add(new SpecialistAppVM(u, crud));
            Date = DateTime.Today;
            foreach (SpecialistAppVM s in Specialists)
                s.SetDate(Date);

        }

        private ICommand _dateCommand;

        public ICommand DateCommand
        {
            get
            {
                if (_dateCommand == null)
                    _dateCommand = new RelayCommand(obj =>
                    {
                        foreach (SpecialistAppVM s in Specialists)
                            s.SetDate(Date);
                    });
                return _dateCommand;
            }
            set { _dateCommand = value; }
        }

        private ICommand _newAppCommand;

        public ICommand NewAppCommand
        {
            get
            {
                if (_newAppCommand == null)
                    _newAppCommand = new RelayCommand(obj =>
                    {
                        nav.Navigate(new View.MakeAppointmentPage(new MakeAppointmentVM(crud)));
                    });
                return _newAppCommand;
            }
            set { _newAppCommand = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

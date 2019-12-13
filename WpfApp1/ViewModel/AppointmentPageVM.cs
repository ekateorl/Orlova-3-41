using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DAL.Entities;
using DAL.Interfaces;

namespace WpfApp1.ViewModel
{
    public class AppointmentPageVM : INotifyPropertyChanged
    {
        IDbRepository crud;

        private ObservableCollection<SpecialistAppVM> specialists;

        public ObservableCollection<SpecialistAppVM> Specialists {
            get { return specialists; }
            set
            {
                specialists = value;
                OnPropertyChanged("Specialists");
            }
        }

        public DateTime Date { get; set; }

        public AppointmentPageVM(IDbRepository crud)
        {
            this.crud = crud;
            Specialists = new ObservableCollection<SpecialistAppVM>();
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


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

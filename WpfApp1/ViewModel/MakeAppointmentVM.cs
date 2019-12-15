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
    public class MakeAppointmentVM : INotifyPropertyChanged
    {
        IDbRepository crud;

        public DateTime Date { get; set; }

        public Category SelectedCategory { get; set; }

        public Service SelectedService { get; set; }

        public List<Category> Categories { get; set; }

        private List<Service> services;
        public List<Service> Services
        {
            get { return services; }
            set
            {
                services = value;
                OnPropertyChanged("Services");
            }
        }

        private ObservableCollection<SpecialistAppVM> specialists;
        public ObservableCollection<SpecialistAppVM> Specialists
        {
            get { return specialists; }
            set
            {
                specialists = value;
                OnPropertyChanged("Specialists");
            }

        }

        public MakeAppointmentVM(IDbRepository crud)
        {
            this.crud = crud;
            Categories = crud.Categories.GetList();
            Specialists = new ObservableCollection<SpecialistAppVM>();
        }

        private ICommand _categoryCommand;

        public ICommand CategoryCommand
        {
            get
            {
                if (_categoryCommand == null)
                    _categoryCommand = new RelayCommand(obj =>
                    {
                        
                        Services = crud.MakeAppointments.SelectServices(SelectedCategory.CategoryId);
                    });
                return _categoryCommand;
            }
            set { _categoryCommand = value; }
        }

        private ICommand _serviceCommand;

        public ICommand ServiceCommand
        {
            get
            {
                if (_serviceCommand == null)
                    _serviceCommand = new RelayCommand(obj =>
                    {
                        if (Specialists != null)
                            Specialists.Clear();
                        if (SelectedService != null)
                        {
                            List<User> users = crud.MakeAppointments.SelectSpecialists(SelectedService.ServiceId);
                            foreach (User u in users)
                            {
                                Specialists.Add(new SpecialistAppVM(u, crud));
                                Specialists.Last().NewApp += AddNewAppointment;
                            }
                            foreach (SpecialistAppVM s in Specialists)
                                s.SelectTime(Date, SelectedService);
                        }
                    });
                return _serviceCommand;
            }
            set { _serviceCommand = value; }
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
                            s.SelectTime(Date, SelectedService);
                    });
                return _dateCommand;
            }
            set { _dateCommand = value; }
        }

        void AddNewAppointment(TimeSlot timeSlot)
        {
            Appointment appointment = new Appointment
            {
                Done = false,
                Price = SelectedService.Price,
                ClientFk = 1,
                ServiceFk = SelectedService.ServiceId,
                Service = SelectedService,
                ReceptionistFk = 5,
                TimeSlotFk = timeSlot.TimeSlotId,
                TimeSlot = timeSlot
            };
            crud.Appointments.Create(appointment);
            var ts = crud.MakeAppointments.SelectTime(timeSlot.WorkDayFk, timeSlot.Beginning).ToList();
            TimeSpan duration = SelectedService.Duration;
            TimeSpan time = new TimeSpan(0);
            for (int i = 0; i < ts.Count && time <= duration; i++)
            {
                ts.ElementAt(i).AppointmentFk = appointment.AppointmentId;
                ts.ElementAt(i).Appointment1 = appointment;
                time += ts.ElementAt(i).Duration;
            }
            crud.Save();

            foreach (SpecialistAppVM s in Specialists)
                s.SelectTime(Date, SelectedService);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

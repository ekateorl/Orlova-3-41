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
using WpfApp1.ViewModel;
using WpfApp1.View;

namespace WpfApp1.ViewModel
{
    public class SpecialistAppVM : INotifyPropertyChanged
    {
        IDbRepository crud;

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        private string time;

        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }

        private ObservableCollection<AppointmentVM> appointments; 

        public ObservableCollection<AppointmentVM> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }

        private ObservableCollection<TimeSlot> timeSlots;

        public ObservableCollection<TimeSlot> TimeSlots
        {
            get { return timeSlots; }
            set
            {
                timeSlots = value;
                OnPropertyChanged("TimeSlots");
            }
        }

        public SpecialistAppVM(User u, IDbRepository crud)
        {
            this.crud = crud;
            UserId = u.UserId;
            Name = u.Name;
            Image = u.Image;
        }

        public void SetDate(DateTime date)
        {
            List<WorkDay> workDays = crud.WorkDays.GetList().Where(i=>i.Date==date && i.SpecialistFk==UserId).ToList();
            if (workDays.Count > 0) 
            {
                WorkDay workDay = workDays.Last();
                Time = workDay.beginning.ToString();
                Time = Time.Substring(0, Time.Length - 3);
                Time += "-" + workDay.ending.ToString();
                Time = Time.Substring(0, Time.Length - 3);
                Appointments = new ObservableCollection<AppointmentVM>();
                List<Appointment> app = crud.Appointments.GetList().Where(i => i.TimeSlot.WorkDay.SpecialistFk == UserId &&
                i.TimeSlot.WorkDayFk == workDay.WorkDayId).OrderBy(i=>i.TimeSlot.Beginning).ToList();
                foreach (Appointment a in app)
                    Appointments.Add(new AppointmentVM(a, crud));
            }
            else
            {
                Time = "Выходной";
                Appointments = null;
            }
            
        }

        public void SelectTime(DateTime date, Service s)
        {
            List<WorkDay> workDays = crud.WorkDays.GetList().Where(i => i.Date == date && i.SpecialistFk == UserId).ToList();
            if (workDays.Count > 0)
            {
                WorkDay workDay = workDays.Last();
                Time = workDay.beginning.ToString();
                Time = Time.Substring(0, Time.Length - 3);
                Time += "-" + workDay.ending.ToString();
                Time = Time.Substring(0, Time.Length - 3);
                List<TimeSlot> timeS = crud.MakeAppointments.SelectTime(workDay.WorkDayId, s.ServiceId).ToList();
                TimeSlots = new ObservableCollection<TimeSlot>();
                foreach (TimeSlot ts in timeS)
                    TimeSlots.Add(ts);
            }
            else
            {
                Time = "Выходной";
                TimeSlots = null;
            }

        }

        private ICommand _removeCommand;

        public ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null)
                    _removeCommand = new RelayCommand(obj =>
                    {
                        var dialog = new AskDialog(new DialogVM("Вы уверены, что хотите удалить запись?"));
                        if (dialog.ShowDialog() == true)
                        {
                            AppointmentVM app = (AppointmentVM)obj;
                            List<TimeSlot> timeSlots = crud.TimeSlots.GetList().Where(i => i.AppointmentFk == app.AppointmentId).ToList();
                            foreach (TimeSlot t in timeSlots)
                                t.AppointmentFk = null;
                            crud.Appointments.Delete(app.AppointmentId);
                            Appointments.Remove(app);
                            crud.Save();
                        }
                    });
                return _removeCommand;
            }
            set { _removeCommand = value; }
        }

        private ICommand _payCommand;

        public ICommand PayCommand
        {
            get
            {
                if (_payCommand == null)
                    _payCommand = new RelayCommand(obj =>
                    {
                        AppointmentVM app = (AppointmentVM)obj;
                        int i =Appointments.IndexOf(app);
                        appointments.Remove(app);
                        Appointment a = crud.Appointments.GetItem(app.AppointmentId);
                        a.Done = true;
                        app.Done = true;
                        app.Paid= "Оплачено";
                        crud.Appointments.Update(a);
                        crud.Save();
                        appointments.Insert(i, app);
                    });
                return _payCommand;
            }
            set { _payCommand = value; }
        }

        public delegate void NewAppointment(TimeSlot timeSlot);
        public NewAppointment NewApp { get; set; }

        private ICommand _makeAppCommand;

        public ICommand MakeAppCommand
        {
            get
            {
                if (_makeAppCommand == null)
                    _makeAppCommand = new RelayCommand(obj =>
                    {
                        if (NewApp != null)
                            NewApp((TimeSlot)obj);
                    });
                return _makeAppCommand;
            }
            set { _makeAppCommand = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

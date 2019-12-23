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
    public class Week
    {
        public DateTime monday { get; set; }
        public Week(DateTime monday)
        {
            this.monday = monday;
        }
        public string Monday
        { get { return monday.ToShortDateString(); } }
        public string Tuesday
        { get { return monday.AddDays(1).ToShortDateString(); } }
        public string Wednesday
        { get { return monday.AddDays(2).ToShortDateString(); } }
        public string Thursday
        { get { return monday.AddDays(3).ToShortDateString(); } }
        public string Friday
        { get { return monday.AddDays(4).ToShortDateString(); } }
        public string Saturday
        { get { return monday.AddDays(5).ToShortDateString(); } }
        public string Sunday
        { get { return monday.AddDays(6).ToShortDateString(); } }
    }
    public class SchedulePageVM : INotifyPropertyChanged
    {
        IDbRepository crud;
        List<User> users;
        List<WorkDayVM> workDays;
        private ObservableCollection<SpecialistWorkWeekVM> specialists;
        public ObservableCollection<SpecialistWorkWeekVM> Specialists
        {
            get { return specialists; }
            set
            {
                specialists = value;
                OnPropertyChanged("Specialists");
            }
        }
        public List<WorkDay> SelectedWorkDays { get; set; }
        Week week;
        public Week Week {
            get { return week; }
            set
            {
                week = value;
                OnPropertyChanged("Week");
            }
        }

        public TimeSpan? Beginning { get; set; }
        public TimeSpan? Ending { get; set; }


        public SchedulePageVM(IDbRepository crud)
        {
            this.crud = crud;
            users = crud.Users.GetList().Where(i => i.UserType.Name == "Мастер").OrderBy(i => i.Name).ToList();
            workDays = new List<WorkDayVM>();
            Week=new Week(DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday));
            Specialists = new ObservableCollection<SpecialistWorkWeekVM>();
            foreach (User u in users)
                Specialists.Add(new SpecialistWorkWeekVM(crud, u, Week.monday));
            Beginning = new TimeSpan(9, 0, 0);
            Ending = new TimeSpan(13, 0, 0);
        }

        private ICommand _prevCommand;
        public ICommand PrevCommand
        {
            get
            {
                if (_prevCommand == null)
                    _prevCommand = new RelayCommand(obj =>
                    {
                        Week=new Week(Week.monday.AddDays(-7));
                        Specialists.Clear();
                        foreach (User u in users)
                            Specialists.Add(new SpecialistWorkWeekVM(crud, u, Week.monday));
                    });
                return _prevCommand;
            }
            set { _prevCommand = value; }
        }

        private ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (_nextCommand == null)
                    _nextCommand = new RelayCommand(obj =>
                    {
                        Week = new Week(Week.monday.AddDays(7));
                        Specialists.Clear();
                        foreach (User u in users)
                            Specialists.Add(new SpecialistWorkWeekVM(crud, u, Week.monday));
                    });
                return _nextCommand;
            }
            set { _nextCommand = value; }
        }

        private ICommand _selectWorkDayCommand;
        public ICommand SelectWokDayCommand
        {
            get
            {
                if (_selectWorkDayCommand == null)
                    _selectWorkDayCommand = new RelayCommand(obj =>
                    {
                        WorkDayVM w = (WorkDayVM)obj;
                        if (!workDays.Contains(w))

                            workDays.Add((WorkDayVM)obj);
                    });
                return _selectWorkDayCommand;
            }
            set { _selectWorkDayCommand = value; }
        }

        private ICommand _removeWorkDayCommand;
        public ICommand RemoveWokDayCommand
        {
            get
            {
                if (_removeWorkDayCommand == null)
                    _removeWorkDayCommand = new RelayCommand(obj =>
                    {
                        WorkDayVM w = (WorkDayVM)obj;
                        if(workDays.Contains(w))
                        workDays.Remove((WorkDayVM)obj);
                    });
                return _removeWorkDayCommand;
            }
            set { _removeWorkDayCommand = value; }
        }

        private ICommand _workDayCommand;
        public ICommand WorkDayCommand
        {
            get
            {
                if (_workDayCommand == null)
                    _workDayCommand = new RelayCommand(obj =>
                    {
                        if (Beginning != null && Ending != null)
                            if (Beginning < Ending)
                            {
                                foreach (WorkDayVM vM in workDays)
                                    if (vM.workDay == null)
                                    {
                                        WorkDay w = new WorkDay();
                                        w.Date = vM.Date;
                                        w.SpecialistFk = vM.Specialist.UserId;
                                        w.beginning = (TimeSpan)Beginning;
                                        w.ending = (TimeSpan)Ending;
                                        crud.WorkDays.Create(w);
                                        w.User = vM.Specialist;
                                        TimeSpan span = new TimeSpan(0, 15, 0);
                                        for (TimeSpan t = w.beginning; t < Ending - span; t += span)
                                        {
                                            TimeSlot timeSlot = new TimeSlot();
                                            timeSlot.Beginning = t;
                                            timeSlot.Duration = span;
                                            timeSlot.WorkDayFk = w.WorkDayId;
                                            crud.TimeSlots.Create(timeSlot);
                                        }
                                        crud.Save();

                                    }
                                    else
                                    {
                                        List<TimeSlot> timeSlots = crud.TimeSlots.GetList().Where(i => i.WorkDayFk == vM.workDay.WorkDayId).ToList();
                                        bool b = true;
                                        foreach (TimeSlot t in timeSlots)

                                            if (t.AppointmentFk != null)
                                            {
                                                b = false;
                                                break;
                                            }
                                        if (b)
                                        {
                                            WorkDay w = crud.WorkDays.GetItem(vM.workDay.WorkDayId);
                                            foreach (TimeSlot t in timeSlots)
                                                crud.TimeSlots.Delete(t.TimeSlotId);
                                            TimeSpan span = new TimeSpan(0, 15, 0);
                                            for (TimeSpan t = w.beginning; t < Ending - span; t += span)
                                            {
                                                TimeSlot timeSlot = new TimeSlot();
                                                timeSlot.Beginning = t;
                                                timeSlot.Duration = span;
                                                timeSlot.WorkDayFk = w.WorkDayId;
                                                crud.TimeSlots.Create(timeSlot);
                                            }
                                            crud.Save();
                                        }
                                        else
                                        {
                                            var dialog = new View.MessageWindow(new DialogVM("Невозможно изменить время для " + vM.workDay.User.Name + "на " + vM.Date.ToLongDateString()));
                                            dialog.ShowDialog();
                                        }
                                    }
                                }
                        Specialists.Clear();
                        foreach (User u in users)
                            Specialists.Add(new SpecialistWorkWeekVM(crud, u, Week.monday));
                    });
                return _workDayCommand;
            }
            set { _workDayCommand = value; }
        }

        private ICommand _deleteWorkDayCommand;
        public ICommand DeleteWorkDayCommand
        {
            get
            {
                if (_deleteWorkDayCommand == null)
                    _deleteWorkDayCommand = new RelayCommand(obj =>
                    {
                        foreach (WorkDayVM vM in workDays)

                            if (vM.workDay != null)
                            {
                                List<TimeSlot> timeSlots = crud.TimeSlots.GetList().Where(i => i.WorkDayFk == vM.workDay.WorkDayId).ToList();
                                bool b = true;
                                foreach (TimeSlot t in timeSlots)

                                    if (t.AppointmentFk != null)
                                    {
                                        b = false;
                                        break;
                                    }
                                if (b)
                                {
                                    WorkDay w = crud.WorkDays.GetItem(vM.workDay.WorkDayId);
                                    foreach (TimeSlot t in timeSlots)
                                        crud.TimeSlots.Delete(t.TimeSlotId);
                                    crud.WorkDays.Delete(w.WorkDayId);
                                    crud.Save();
                                }
                                else
                                {
                                    var dialog = new View.MessageWindow(new DialogVM("Невозможно удалить для " + vM.workDay.User.Name + "на " + vM.Date.ToLongDateString()));
                                    dialog.ShowDialog();
                                }
                            }
                        Specialists.Clear();
                        foreach (User u in users)
                            Specialists.Add(new SpecialistWorkWeekVM(crud, u, Week.monday));

                    });
                return _deleteWorkDayCommand;
            }
            set { _deleteWorkDayCommand = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

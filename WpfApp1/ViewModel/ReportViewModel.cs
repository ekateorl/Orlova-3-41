using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Entities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        IDbRepository crud;

        public List<User> Specialists { get; set; }

        List<ServiceInfo> serviceInfos;
        public List<ServiceInfo> ServiceInfos
        {
            get { return serviceInfos; }
            set
            {
                serviceInfos = value;
                OnPropertyChanged("ServiceInfos");
            }
        }

        List<SpecialistWorkTime> timeInfos;
        public List<SpecialistWorkTime> TimeInfos
        {
            get { return timeInfos; }
            set
            {
                timeInfos = value;
                OnPropertyChanged("TimeInfos");
            }
        }

        public DateTime ServInfoDate1 { get; set; }
        public DateTime ServInfoDate2 { get; set; }

        public DateTime TimeInfoDate1 { get; set; }
        public DateTime TimeInfoDate2 { get; set; }

        private User selectedSpecialistSI;
        public User SelectedSpecialistSI
        {
            get { return selectedSpecialistSI; }
            set
            {
                selectedSpecialistSI = value;
                OnPropertyChanged("SelectedSpecialistSI");
            }
        }

        public ReportViewModel(IDbRepository crud)
        {
            this.crud = crud;
            Specialists = crud.Users.GetList().Where(i => i.UserType.Name == "Мастер").OrderBy(i => i.Name).ToList();
            ServInfoDate1 = DateTime.Today;
            ServInfoDate2 = DateTime.Today;
            TimeInfoDate1 = DateTime.Today;
            TimeInfoDate2 = DateTime.Today;
        }

        private ICommand _serviceInfoCommand;

        public ICommand ServiceInfoCommand
        {
            get
            {
                if (_serviceInfoCommand == null)
                    _serviceInfoCommand = new RelayCommand(obj =>
                    {
                        if (SelectedSpecialistSI == null)
                            ServiceInfos = crud.Reports.ServiceInfos(ServInfoDate1, ServInfoDate2);
                        else
                            ServiceInfos = crud.Reports.ServiceInfos(ServInfoDate1, ServInfoDate2, SelectedSpecialistSI.UserId);
                    });
                return _serviceInfoCommand;
            }
            set { _serviceInfoCommand = value; }
        }

        private ICommand _timeInfoCommand;

        public ICommand TimeInfoCommand
        {
            get
            {
                if (_timeInfoCommand == null)
                    _timeInfoCommand = new RelayCommand(obj =>
                    {
                        TimeInfos = crud.Reports.WorkTimes(TimeInfoDate1, TimeInfoDate2);
                    });
                return _timeInfoCommand;
            }
            set { _timeInfoCommand = value; }
        }

        private ICommand _removeSpecialistCommand;

        public ICommand RemoveSpecialistCommand
        {
            get
            {
                if (_removeSpecialistCommand == null)
                    _removeSpecialistCommand = new RelayCommand(obj =>
                    {
                        SelectedSpecialistSI = null;
                    });
                return _removeSpecialistCommand;
            }
            set { _removeSpecialistCommand = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

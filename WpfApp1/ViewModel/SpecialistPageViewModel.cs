
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Navigation;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace WpfApp1.ViewModel
{
    public class SpecialistPageViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        IDbRepository crud;
        NavigationService nav;
        public List<User> SpecialistList { get; set; }

        private User selectedSpecialist;
        public User SelectedSpecialist
        {
            get { return selectedSpecialist; }
            set
            {
                selectedSpecialist = value;
                OnPropertyChanged("SelectedSpecialist");
                Name = "Name";
            }
        }

        public SpecialistPageViewModel(IDbRepository crud, NavigationService nav)
        {
            this.crud = crud;
            this.nav = nav;
            SpecialistList = crud.Users.GetList().Where(i => i.UserType.Name == "Мастер").ToList();
            
        }
        private ICommand _infoCommand;

        public ICommand InfoCommand
        {
            get
            {
                if (_infoCommand == null)
                    _infoCommand = new RelayCommand(obj =>
                    {
                        nav.Navigate(new View.SpecialistInfo(new SpecialistInfoViewModel(nav, crud, (User)obj)));
                    });
                return _infoCommand;
            }
            set { _infoCommand = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

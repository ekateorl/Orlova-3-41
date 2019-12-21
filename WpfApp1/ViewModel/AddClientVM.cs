using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DAL.Entities;

namespace WpfApp1.ViewModel
{
    public class AddClientVM : IRequireViewIdentification, INotifyPropertyChanged
    {
        private bool filled;
        public bool Filled
        {
            get { return filled; }
            set
            {
                filled = value;
                OnPropertyChanged("Filled");
            }
        }
        public Client Client { get; set; }
        public AddClientVM(Client Client)
        {
            this.Client = Client;
        }
        private Guid _viewId;
        public Guid ViewID
        {
            get { return _viewId; }
        }
        private ICommand _okCommand;

        public ICommand OKCommand
        {
            get
            {
                if (_okCommand == null)
                    _okCommand = new RelayCommand(obj =>
                    {
                        WindowManager.DialogResultTrue(ViewID);
                    });
                return _okCommand;
            }
            set { _okCommand = value; }
        }

        private ICommand _cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new RelayCommand(obj =>
                    {
                        WindowManager.DialogResultFalse(ViewID);
                    });
                return _cancelCommand;
            }
            set { _cancelCommand = value; }
        }

        private ICommand _filledCommand;

        public ICommand FilledCommand
        {
            get
            {
                if (_filledCommand == null)
                    _filledCommand = new RelayCommand(obj =>
                    {
                        Filled = ((string)obj) != "";
                    });
                return _filledCommand;
            }
            set { _filledCommand = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

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
    public class AddClientVM : DialogVM, INotifyPropertyChanged
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

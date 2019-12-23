using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    public class DialogVM : IRequireViewIdentification
    {
        public string Question { get; set; }
        public DialogVM()
        {

        }
        public DialogVM(string Question)
        {
            this.Question = Question;

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
    }
}

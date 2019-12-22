using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using DAL.Entities;
using DAL.Interfaces;

namespace WpfApp1.ViewModel
{
    public class LoginWindowViewModel : IRequireViewIdentification
    {
        public string Login { get; set; }

        public string Password { get; set; }

        IDbRepository crud;

        AuthInfo authInfo;

        public LoginWindowViewModel(IDbRepository crud, AuthInfo authInfo)
        {
            this.crud = crud;
            this.authInfo = authInfo; 
        }

        private ICommand _okCommand;

        public ICommand OKCommand
        {
            get
            {
                if (_okCommand == null)
                    _okCommand = new RelayCommand(obj =>
                    {
                        List<User> users = crud.Users.GetList().Where(i => i.Login == Login && i.Password == Password  &&
                        (i.UserType.Name== "Менеджер" || i.UserType.Name == "Оператор")).ToList();
                        if (users.Count > 0)
                        {
                            authInfo.User = users.First();
                            WindowManager.DialogResultTrue(ViewID);
                        }
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

        private ICommand _passwordChangedCommand;

        public ICommand PasswordChangedCommand
        {
            get
            {
                if (_passwordChangedCommand == null)
                    _passwordChangedCommand = new RelayCommand(obj =>
                    {
                        Password = ((PasswordBox)obj).Password;
                    });
                return _passwordChangedCommand;
            }
            set { _passwordChangedCommand = value; }
        }

        private Guid _viewId;
        public Guid ViewID
        {
            get { return _viewId; }
        }
    }
}

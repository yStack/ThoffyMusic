using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Infrastructure;

namespace ThoffyMusic.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private User _user;
        private string _phoneOrEmail;
        private bool _isLogin;

        private ICommand _loginCommand;


        public User User
        {
            get { return _user; }
            set
            {
                if (value != _user)
                {
                    _user = value;
                    OnPropertyChanged("User");
                }
            }
        }


        public string PhoneOrEmail
        {
            get { return _phoneOrEmail; }
            set
            {
                if (value != _phoneOrEmail)
                {
                    _phoneOrEmail = value;
                    if (_phoneOrEmail.Contains("@"))
                    {
                        _user.Email = _phoneOrEmail;
                    }
                    else
                    {
                        _user.CellPhone = _phoneOrEmail;
                    }
                    OnPropertyChanged("PhoneOrEmail");
                }
            }
        }

        public bool IsLogin
        {
            get { return _isLogin; }
            set
            {
                if (value != _isLogin)
                {
                    _isLogin = value;
                    OnPropertyChanged("IsLogin");
                }
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand((obj) =>
                {
                    IsLogin = _user.Login(0);

                    MessageBox.Show($"Login is {IsLogin.ToString()}");
                }));
            }
        }


        public UserViewModel()
        {
            _user = new User();
        }


    }
}

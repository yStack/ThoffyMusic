using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Infrastructure;
using Infrastructure.UserInfo;

namespace ThoffyMusic.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private User _user;
        private string _phoneOrEmail;
        private bool _isLogin;
        private LoginType _loginType;

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
                        _loginType = LoginType.Email;
                    }
                    else
                    {
                        _user.CellPhone = _phoneOrEmail;
                        _loginType = LoginType.Phone;
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
                    IsLogin = _user.Login(_loginType);
                    var playlist = _user.Playlist;
                    playlist.ForEach((t) => UserPlaylist.Add(t));
                }));
            }
        }

        public ObservableCollection<Playlist> UserPlaylist { get; }
       


        public UserViewModel()
        {
            _user = new User();
            UserPlaylist = new ObservableCollection<Playlist>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Infrastructure;

namespace ThoffyMusic.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private User _user;

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



        public UserViewModel()
        {

        }


    }
}

using Infrastructure.Auth;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using Infrastructure.UserInfo;
using System.Linq;
using System;
using System.ComponentModel;

namespace Infrastructure
{
    public enum LoginType
    {
        Email = 0,
        Phone = 1,
        QrCode = 2,
    }

    /// <summary>
    /// User对象
    /// </summary>
    public class User : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged 接口实现
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        #region 字段
        private LoginContext _loginContext;
        private string _cellPhone;
        private string _email;
        private string _password;

        #endregion


        #region 属性
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string CellPhone
        {
            get { return _cellPhone; }
            set
            {
                if (value != _cellPhone)
                {
                    _cellPhone = value;
                    OnPropertyChanged("CellPhone");
                }
            }
        }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email
        {
            get { return _email; }
            set
            {
                if (value != _email)
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        /// <summary>
        /// 用户输入的密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged("PassWord");
                }
            }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int ID { get; set; } = -1;


        /// <summary>
        /// 播放列表
        /// </summary>
        public List<Playlist> Playlist
        {
            get
            {
                if (ID != -1)
                {
                    var resp = UrlHelper.Get(UrlHelper.RootUrl + $"/user/playlist?uid={ID}");
                    return JsonHelper.GetPlaylist(resp);
                }
                throw new Exception("user's id is incorrect");
            }
        }

        #endregion

        public User()
        {

        }

        #region 公开方法
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Login(LoginType type)
        {
            ILoginStrategy strategy = null;
            switch (type)
            {
                case LoginType.Email:
                    strategy = new EmailLogin();
                    break;
                case LoginType.Phone:
                    strategy = new PhoneLogin();
                    break;
                case LoginType.QrCode:
                    strategy = new QrCodeLogin();
                    break;
            }

            _loginContext = new LoginContext(strategy, this);
            JObject j = _loginContext.Login();

            //解析返回值
            AuthInfoHelper auth = new AuthInfoHelper(j);
            if (auth.Code == 200)
            {
                ID = auth.UserID;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public bool Logout()
        {
            return LoginContext.Logout();
        }

        #endregion


    }
}

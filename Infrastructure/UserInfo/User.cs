using Infrastructure.Auth;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

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
    public class User
    {
        #region fields
        private LoginContext _loginContext;
        #endregion


        #region properties
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string CellPhone { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户输入的密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int ID { get; set; } = -1;

        #endregion


        #region public methods
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
            return _loginContext.Logout();
        }

        public List<string> GetPlaylist()
        {
            var resp = UrlHelper.Get(UrlHelper.RootUrl + $"/user/playlist?uid={ID}");
            using (StreamWriter sw = new StreamWriter("D:/playlist.txt", true))
            {
                sw.WriteLine(JObject.Parse(resp).ToString());
            }
             return new List<string>();
        }

        #endregion


    }
}

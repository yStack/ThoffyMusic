using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    /// <summary>
    /// 策略模式Context类
    /// </summary>
    class LoginContext
    {
        //登录策略
        private ILoginStrategy _loginStrategy;

        //user对象
        private User _user;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="loginStrategy"></param>
        /// <param name="user"></param>
        public LoginContext(ILoginStrategy loginStrategy, User user)
        {
            _loginStrategy = loginStrategy;
            _user = user;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isQrLogin"></param>
        /// <returns></returns>
        public bool Login(User user, bool isQrLogin)
        {
            return _loginStrategy.Login(user.Email, user.CellPhone, isQrLogin, user.Password);
        }

    }

    interface ILoginStrategy
    {
        /// <summary>
        /// 用户登录的方法接口
        /// </summary>
        /// <param name="email">email地址</param>
        /// <param name="cellPhone">手机号码</param>
        /// <param name="qrCode"></param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        bool Login(string email, string cellPhone, bool isQrLogin, string password);
    }


    public class EmailLogin : ILoginStrategy
    {
        public bool Login(string email, string cellPhone, bool isQrLogin, string password)
        {
            // 检查输入是否合法
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return false;

            string timeStamp = UrlHelper.GetTimeStamp().ToString();
            var url = UrlHelper.RootUrl + $"/login?timeStamp={timeStamp}";
           
            
            return false;
        }
    }




}

using Infrastructure.Auth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Auth
{
    /// <summary>
    /// 策略模式Context类
    /// </summary>
    public class LoginContext
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
        public JObject Login(User user, bool isQrLogin)
        {
            return _loginStrategy.Login(user.Email, user.CellPhone, isQrLogin, user.Password);
        }

        public bool QrCheck(string key)
        {
            string checkUrl = UrlHelper.RootUrl + $"/login/qr/check?key={key}";
           
            while (true)
            {
                var resp = UrlHelper.Get(checkUrl);
                JObject j = JObject.Parse(resp);
                if (j.Value<int>("code") == 803)
                {
                    //登录成功
                    //Todo:
                }
                Thread.Sleep(50);
            }

        }

    }

    public interface ILoginStrategy
    {
        /// <summary>
        /// 用户登录的方法接口
        /// </summary>
        /// <param name="email">email地址</param>
        /// <param name="cellPhone">手机号码</param>
        /// <param name="qrCode"></param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        JObject Login(string email, string cellPhone, bool isQrLogin, string password);
      
    }


    public class EmailLogin : ILoginStrategy
    {

        public JObject Login(string email, string cellPhone, bool isQrLogin, string password)
        {
            // 检查输入是否合法
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Input param not corrent");

            // 组装url
            string timeStamp = UrlHelper.GetTimeStamp().ToString();
            var url = UrlHelper.RootUrl + $"/login?timeStamp={timeStamp}";

            //组装post数据
            string md5Password = UrlHelper.Md5(password);
            var user = new { email = email, md5_password = md5Password };
            string jsonStr = JsonConvert.SerializeObject(user);

            //post请求
            string resp = UrlHelper.Post(url, jsonStr);

            //返回值
            var respObj = JObject.Parse(resp);
            return respObj;
        }
    }

    public class PhoneLogin : ILoginStrategy
    {

        public JObject Login(string email, string cellPhone, bool isQrLogin, string password)
        {
            // 检查输入是否合法
            if (string.IsNullOrEmpty(cellPhone) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Input param not corrent");

            // 组装url
            string timeStamp = UrlHelper.GetTimeStamp().ToString();
            var url = UrlHelper.RootUrl + $"/login/cellphone?timeStamp={timeStamp}";

            //组装post数据
            string md5Password = UrlHelper.Md5(password);
            var user = new { phone = cellPhone, md5_password = md5Password };
            string jsonStr = JsonConvert.SerializeObject(user);

            //post请求
            string resp = UrlHelper.Post(url, jsonStr);

            //返回值
            var respObj = JObject.Parse(resp);
            return respObj;
        }
    }


    public class QrCodeLogin : ILoginStrategy
    {
        public JObject Login(string email, string cellPhone, bool isQrLogin, string password)
        {
            // 检查输入是否合法
            if (isQrLogin != true)
                throw new ArgumentException("Input param not corrent");

            // 获取key
            string getKeyUrl = UrlHelper.RootUrl + "/login/qr/key";
            string keyJson = UrlHelper.Get(getKeyUrl);
            JObject j = JObject.Parse(keyJson);
            int code = j["code"].Value<int>();
            if (code != 200)
            {
                throw new Exception("can not get a key");
            }
            var key = j["data"]["unikey"].Value<string>();


            // 获取QRCode
            string createQrCodeUrl = UrlHelper.RootUrl + $"/login/qr/create?key={key}&qrimg=true";
            string qrCodeJson = UrlHelper.Get(createQrCodeUrl);
            j = JObject.Parse(qrCodeJson);
            code = j["code"].Value<int>();
            if (code != 200)
            {
                throw new Exception("can not get a QRCode");
            }
            var qrCode = j["data"]["qrimg"].Value<string>();

            var result = new { key = key, qrCode = qrCode };
            var jObj = JObject.Parse(JsonConvert.SerializeObject(result).ToString());
            return jObj;
        }
    }
}

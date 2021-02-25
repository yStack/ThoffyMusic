using System;
using Infrastructure;
using Infrastructure.Auth;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailLogin e = new EmailLogin();
            string email = "vonfat@126.com";
            string pass = "wy&86147965";
            string phone = "15295774040";
            //var t =  e.Login(email, "", false, pass);

            QrCodeLogin p = new QrCodeLogin();
            var t = p.Login("", phone, true, pass);

            User user = new Infrastructure.User();

            LoginContext lc = new LoginContext(p, user);
            var f = lc.Login(user, true);
            string key = f.Value<string>("key");
            string qr = f.Value<string>("qrCode");

            lc.QrCheck(key);


            //AuthInfoHelper at = new AuthInfoHelper(t);
            //int code = at.Code;
            //string msg = at.ErrMsg;
            //Console.WriteLine(code);
            Console.ReadKey();
        }
    }
}

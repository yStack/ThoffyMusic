using System;
using Infrastructure;
using Infrastructure.Auth;
using Un4seen.Bass;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            //string email = "vonfat@126.com";
            //string pass = "wy&86147965";
            //string phone = "15295774040";
            //User user = new Infrastructure.User();
            //user.Email = email;
            //user.CellPhone = phone;
            //user.Password = pass;

            //var t = user.Login(0);
            //user.GetPlaylist();

            string url = "http://m8.music.126.net/20210226224613/97dbc14ae4892b7d946c66db0f900c39/ymusic/0fd6/4f65/43ed/a8772889f38dfcb91c04da915b301617.mp3";
            BassNet.Registration("vonfat@gmail.com", "2X17241816152222");

            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
            {
                var stream = Bass.BASS_StreamCreateURL(url, 0, BASSFlag.BASS_SAMPLE_FLOAT, null, IntPtr.Zero);
                Bass.BASS_ChannelPlay(stream, false);
            }
      
      

            Console.ReadKey();


        }
    }
}

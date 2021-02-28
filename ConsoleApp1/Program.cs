using System;
using Infrastructure;
using Infrastructure.Auth;
using Un4seen.Bass;
using BassEngine;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string email = "vonfat@126.com";
            string pass = "wy&86147965";
            string phone = "15295774040";
            User user = new Infrastructure.User();
            user.Email = email;
            user.CellPhone = phone;
            user.Password = pass;

            var t = user.Login(0);
            var lst = user.Playlist;
            var testLst = lst[0];
            var songlist = testLst.SongList;

            var song = songlist[1];

            var s = song.GetSongUrl();

            BassPlayer.Instance.Init();
            BassPlayer.Instance.Play(s);


   


            Console.ReadKey();


        }
    }
}

﻿using Infrastructure.UserInfo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Infrastructure
{

    class JsonHelper
    {
        /// <summary>
        /// 解析状态码
        /// </summary>
        /// <param name="jstr"></param>
        /// <returns></returns>
        public static int GetCode(string jstr)
        {
            JObject j = JObject.Parse(jstr);
            int code = j.Value<int>("code");
            return code;
        }


        /// <summary>
        /// 保存json到文件
        /// </summary>
        /// <param name="jstr"></param>
        /// <param name="path"></param>
        public static void Write(string jstr, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                JObject j = JObject.Parse(jstr);
                sw.WriteLine(j.ToString());
            }
        }


        /// <summary>
        /// 获取歌单
        /// </summary>
        /// <param name="jstr"></param>
        /// <returns></returns>
        public static List<Playlist> GetPlaylist(string jstr)
        {
            JObject j = JObject.Parse(jstr);

            var playlist = j.SelectToken("playlist").Select(t =>
            {
                string name = t["name"].Value<string>();
                UInt64 id = t["id"].Value<UInt64>();
                return new Playlist(name, id);
            }).ToList();

            return playlist;
        }


        /// <summary>
        /// 解析歌单
        /// </summary>
        /// <param name="jstr"></param>
        /// <returns></returns>
        public static List<Song> GetSongList(string jstr)
        {
            JObject j = JObject.Parse(jstr);
            var songList = j["playlist"]["trackIds"].Select(t =>
            {
                UInt64 id = t["id"].Value<UInt64>();
                UInt32 v = t["v"].Value<UInt32>();
                return new Song(string.Empty, id, v);
            }).ToList();
            return songList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jstr"></param>
        /// <returns></returns>
        public static List<string> GetSongUrl(string jstr)
        {
            JObject j = JObject.Parse(jstr);
            var songUrl = j["data"].Select(t => { return t["url"].Value<string>(); }).ToList();
            return songUrl;
        }
    }
}

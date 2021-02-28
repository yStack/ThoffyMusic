using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Infrastructure.UserInfo
{
    public class Song : InfoBase
    {
        public UInt32 V { get; set; }

        public Song(string name, UInt64 id) : base(name, id)
        {

        }

        public Song(string name, UInt64 id, UInt32 v) : base(name, id)
        {
            V = v;
        }


        public string GetSongUrl(int songRate = 999000)
        {
            var songJson = UrlHelper.Get(UrlHelper.RootUrl + $"/song/url?id={this.Id}&br={songRate}");
            //JsonHelper.Write(songJson, "D:/songUrl.json");
            return JsonHelper.GetSongUrl(songJson)[0];
        }


        public static List<string> GetSongUrls(Playlist playlist, int songRate = 999000)
        {
            // 获取歌单中歌曲的id列表
            List<UInt64> idLst = playlist.SongList.Select(t => t.Id).ToList();

            // 组装id url
            StringBuilder sb = new StringBuilder();
            foreach (var id in idLst)
            {
                sb.Append(id.ToString());
                sb.Append(",");
            }
            string ids = sb.ToString().Remove(sb.Length - 1, 1);

            // 请求歌曲url
            var songJson = UrlHelper.Get(UrlHelper.RootUrl + $"/song/url?id={ids}&br={songRate}");
           
            return JsonHelper.GetSongUrl(songJson);
        }
    }
}

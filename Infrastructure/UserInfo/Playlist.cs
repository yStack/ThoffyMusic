using System;
using System.Collections.Generic;

namespace Infrastructure.UserInfo
{
    /// <summary>
    /// 账号中的播放列表
    /// </summary>
    public class Playlist : InfoBase
    {
        public List<Song> GetSongList()
        {
            var resp = UrlHelper.Get(UrlHelper.RootUrl + $"/playlist/detail?id={this.Id}");
            return JsonHelper.GetSongList(resp);
        }

        public Playlist(string name, UInt64 id) : base(name, id)
        {
          
        }
    }
}

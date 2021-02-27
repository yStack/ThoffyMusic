using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.UserInfo
{
    public class Song : InfoBase
    {
        public UInt32 V { get; set; }

        public Song(string name, UInt64 id):base(name, id)
        {

        }

        public Song(string name, UInt64 id, UInt32 v) : base(name, id)
        {
            V = v;
        }
    }
}

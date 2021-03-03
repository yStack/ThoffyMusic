using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.UserInfo
{
    public class InfoBase : ViewModelBase
    {
        private string _name;
        private UInt64 _id;

        public string Name
        {
            get;
            set;
        }

        public UInt64 Id { get; set; }

        public InfoBase()
        {

        }

        public InfoBase(string name, UInt64 id)
        {
            Name = name;
            Id = id;
        }
    }
}

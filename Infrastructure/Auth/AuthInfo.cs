using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Auth
{
    class AuthInfoHelper
    {
        private JObject _respObj;

        public AuthInfoHelper(JObject respObj)
        {
            _respObj = respObj;
        }

        /// <summary>
        /// 状体码
        /// </summary>
        public int Code
        {
            get
            {
                if(_respObj != null)
                {
                    var jtoken = _respObj.GetValue("code");
                    int code = jtoken.Value<int>();
                    return code;
                }
                return -1;
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg
        {
            get
            {
                if (Code != 200 && Code != 400)
                {
                    var jtoken = _respObj.GetValue("message");
                    string msg = jtoken.Value<string>();
                    return msg;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// UserID
        /// </summary>
        public int UserID
        {
            get
            {
                var jtoken = _respObj.GetValue("account");
                var uid = jtoken.Value<int>("id");
                return uid;
            }
        }

    }
}

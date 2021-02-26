using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}

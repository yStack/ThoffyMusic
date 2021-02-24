using System;
using System.IO;
using System.Net;
using System.Text;
using System.Security.Cryptography;


namespace Infrastructure
{
    class UrlHelper
    {
        public static readonly string RootUrl = "http://localhost:3000";

        /// <summary>
        /// GET方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }

            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            return retString;
        }

        /// <summary>
        /// POST 方法
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static string Post(string url, string jsonStr)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Timeout = 5000;

            // 
            byte[] data = Encoding.UTF8.GetBytes(jsonStr);
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
            }

            // 获取返回值
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            using (StreamReader reader = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
            {
                string respStr = reader.ReadToEnd();
                return respStr;
            }
        }

        /// <summary>
        /// 当前时间戳
        /// </summary>
        /// <returns></returns>
        public static int GetTimeStamp()
        {
            var startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            return (int)(DateTime.Now - startTime).TotalSeconds;
        }

        /// <summary>
        /// 字符串转小写32位MD5字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Md5(string str)
        {
            var md5 = MD5.Create();
            var hashByte = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var hashByte2Str = BitConverter.ToString(hashByte);
            var result = hashByte2Str.Replace("-", "").ToLower();
            md5.Dispose();
            return result;
        }
    }
}

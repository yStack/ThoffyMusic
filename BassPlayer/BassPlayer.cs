using System;
using Un4seen.Bass;

namespace BassEngine
{
    public class BassPlayer
    {
        #region 单例模式
        private static readonly BassPlayer _instance = new BassPlayer();

        private BassPlayer()
        {

        }

        static BassPlayer()
        {

        }

        public static BassPlayer Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// 播放器初始化
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            //注册bass组件
            BassNet.Registration("vonfat@gmail.com", "2X17241816152222");

            //初始化
            return Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
        }

        /// <summary>
        /// 播放url
        /// </summary>
        /// <param name="url"></param>
        public void Play(string url)
        {
            var stream = Bass.BASS_StreamCreateURL(url, 0, BASSFlag.BASS_SAMPLE_FLOAT, null, IntPtr.Zero);
            Bass.BASS_ChannelPlay(stream, false);
        }


        /// <summary>
        /// 关闭播放器
        /// </summary>
        public void Close()
        {
            Bass.FreeMe();
        }
    }
}

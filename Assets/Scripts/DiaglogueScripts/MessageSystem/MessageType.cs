using UnityEngine;
using System.Collections;


namespace MessageSystem
{
    public class MessageType
    {
        private const uint SYSTEM = 0;
        public const uint SYSTEMDEBUGLOG = SYSTEM + 1;

        /// <summary>
        /// UI 消息
        /// </summary>
        private const uint UI = 2000;
        public const uint UIWINDOWCLOSE = UI + 1;

        public const uint UI_UPDATE_TALKWINDOW = UIWINDOWCLOSE;
        public const uint UI_REQUEST_NEXTTALK = UI_UPDATE_TALKWINDOW + 1;

        private const uint AUDIO = 4000;

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        public const uint PLAYBACKGROUNDMUSIC = AUDIO + 1;

        /// <summary>
        /// 播放音效
        /// </summary>
        public const uint PLAYAUDIO = PLAYBACKGROUNDMUSIC + 1;

        /// <summary>
        /// 背景音乐淡入
        /// </summary>
        public const uint BACKGROUNDMUSICFADEIN = PLAYAUDIO + 1;

        /// <summary>
        /// 背景音乐淡出
        /// </summary>
        public const uint BACKGROUNDMUSICFADEOUT = BACKGROUNDMUSICFADEIN + 1;

        public const uint BACKGROUNDMUSICVOLUME = BACKGROUNDMUSICFADEOUT + 1;
        public const uint BACKGROUNDMUSICVOLUMESWITCH = BACKGROUNDMUSICVOLUME + 1;

        public const uint AUDIOVOLUME = BACKGROUNDMUSICVOLUMESWITCH + 1;
        public const uint AUDIOVOLUMESWITCH = AUDIOVOLUME + 1;
        
    }

    //public enum MessageType
    //{
    //    SYSTEM = 0,
    //    SYSTEMDEBUGLOG = SYSTEM + 1,
    //}
}


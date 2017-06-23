using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MessageSystem
{
    public class BaseData
    {
        public uint messageId;
        public object messageData;

        public BaseData(uint type, object data)
        {
            this.messageId = type;
            this.messageData = data;
        }

        public BaseData(uint type)
        { 
        
        }
    }

    public struct Data_Window
    {
        //窗口ID
        public int windowId;

        //窗口状态
        public bool windowState;

        public string name;

        public Data_Window(int id, bool state)
        {
            this.windowId = id;
            this.windowState = state;
            this.name = null;
        }

        public Data_Window(string name, bool state)
        {
            this.windowId = -1;
            this.windowState = state;
            this.name = name;
        }

    }


    public struct TalkData
    {
        public string name;
        public string text;
        public RawImage icon;

        public TalkData(string name, string text, RawImage image)
        {
            this.name = name;
            this.text = text;
            this.icon = image;
        }
    }
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MessageSystem
{
    public class MessageCenter : MonoBehaviour
    {
        //消息队列
        protected static Queue<BaseData> MessageQueue;

        //所有的接收者
        protected static Dictionary<GameObject, IMessageHandleCall> Receivers;

        // Use this for initialization
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        static void Init()
        {
            GameObject gobj = new GameObject("MessageSystem");
            gobj.AddComponent<MessageCenter>();

            MessageQueue = new Queue<BaseData>();
            Receivers = new Dictionary<GameObject, IMessageHandleCall>();
        }


        // Update is called once per frame
        void Update()
        {

            DistributeMessage();
        }

        /// <summary>
        /// 分发消息
        /// </summary>
        protected void DistributeMessage()
        {
            //有问题   
            //foreach (GameObject gobj in Receivers.Keys)
            //{
            //    while (MessageQueue.Count > 0 )
            //    {
            //        IMessageHandleCall call = Receivers[gobj];

            //        BaseData data = MessageQueue.Dequeue();

            //        call.ReceiverMessage(data.messageId, data.messageData);

            //    }
            //}

            List<GameObject> gobjs = new List<GameObject>(Receivers.Keys);
            

            //将每一条信息 分发给所有接收者
            while (MessageQueue.Count > 0 && Receivers.Count > 0)
            {
                BaseData data = MessageQueue.Dequeue();

                foreach (GameObject gobj in gobjs)
                {
                    if (gobj == null) continue;

                    IMessageHandleCall call = Receivers[gobj];

                    call.ReceiverMessage(data.messageId, data.messageData);
                }
            }
            
        }


        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="data">数据</param>
        public static void ReceiveMessage(BaseData data)
        {
            if (MessageQueue == null) Init();
            MessageQueue.Enqueue(data);
        }


        /// <summary>
        /// 注册接收者
        /// </summary>
        /// <param name="gobj">接收者GameObject</param>
        /// <param name="call">回调</param>
        public static void RegisteredReceiver(GameObject gobj, IMessageHandleCall call)
        {
            if (Receivers == null) Init();

            if (Receivers.ContainsKey(gobj))
            {
                //移除原有信息
                Receivers.Remove(gobj);

                //重新添加到字典中
                Receivers.Add(gobj, call);
            }
            else
            {
                //添加到接收者字典中
                Receivers.Add(gobj, call);
                Debug.Log("注册成功");
            }
        }

        /// <summary>
        /// 注销接收者
        /// </summary>
        /// <param name="gobj">接收者GameObject</param>
        public static void CancellationReceiver(GameObject gobj)
        {
            if (Receivers.ContainsKey(gobj))
            {
                Receivers.Remove(gobj);
            }
        }
    }
}


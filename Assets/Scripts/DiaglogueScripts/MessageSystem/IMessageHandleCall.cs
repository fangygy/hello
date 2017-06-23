using UnityEngine;
using System.Collections;

namespace MessageSystem
{
    public interface IMessageHandleCall
    {
        void ReceiverMessage(uint messageID, object data);
    }
}
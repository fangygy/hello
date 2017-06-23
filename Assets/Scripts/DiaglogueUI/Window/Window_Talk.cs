using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using MessageSystem;
using UnityEngine.EventSystems;

public class Window_Talk : MonoBehaviour ,IMessageHandleCall,IPointerClickHandler{

    public Text text_Talk;
    public Text text_Name;
    public RawImage image_Icon;

    Vector3 startPos;
    void Awake()
    { 
        MessageCenter.RegisteredReceiver(gameObject,this);
        startPos = transform.position;
    }
	// Use this for initializations
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void ReceiverMessage(uint messageID, object data)
    {
        if (messageID == MessageType.UI_UPDATE_TALKWINDOW)
        {
            if (data == null)
            {
                transform.position = new Vector3(0, -200, 0);
            }
            else
            {
                transform.position = startPos;

                if (data is TalkData)
                {
                    TalkData td = (TalkData)data;
                    text_Name.text = td.name;
                    text_Talk.text = td.text;
                }
            }


            
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("pointer click");
        if (eventData.button == PointerEventData.InputButton.Left)
        { 
            //请求下一条数据
            MessageCenter.ReceiveMessage(new BaseData(MessageType.UI_REQUEST_NEXTTALK, null));
        }
    }
}

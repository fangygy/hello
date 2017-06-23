using UnityEngine;
using System.Collections;
using MessageSystem;

public class StoryDialogueSystem : MonoBehaviour,IMessageHandleCall {

    public static StoryDialogueSystem Instance;

    Table data;

    Table current_Talk;
    int currentIndex;
    void Awake()
    {
        Instance = this;
        MessageCenter.RegisteredReceiver(gameObject, this);
    }


	// Use this for initialization
	void Start () {

        data = DataManager.allTables[DataManager.TableName.对话系统数据];

        //根据场景开始不同的对话
        StartTalk("1001001");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartTalk(string id)
    {
        //剧本的Id名字
        string name = data.GetDataWithIDAndIndex(id, 1);
        //根据Id获取Table
        current_Talk = new Table(Application.dataPath + "/Config/TalkData/" + name + ".txt");

        //获取Table的第2行
        string[] temp = current_Talk.GetLineWithIndex(currentIndex = 1); //urrentIndex = 1
        //将数据打包
        TalkData tdata = new TalkData(temp[0],temp[2],null);
        //发送数据给window
        MessageCenter.ReceiveMessage(new BaseData(MessageType.UI_UPDATE_TALKWINDOW, tdata));
    }

    public void NextTalk()
    {
        //当前的Table根据currentIndex向下读取数据
        string[] temp = current_Talk.GetLineWithIndex(++currentIndex);

        //如果数据不存在，则发送null
        if (temp == null)
        {
            MessageCenter.ReceiveMessage(new BaseData(MessageType.UI_UPDATE_TALKWINDOW, null));
        }
        //如果存在，则发送数据
        else
        {
            TalkData tdata = new TalkData(temp[0], temp[2], null);

            MessageCenter.ReceiveMessage(new BaseData(MessageType.UI_UPDATE_TALKWINDOW, tdata));
        }

       
    }

    public void ReceiverMessage(uint messageID, object data)
    {
        if (messageID == MessageType.UI_REQUEST_NEXTTALK)
        {
            NextTalk();
        }
    }
}

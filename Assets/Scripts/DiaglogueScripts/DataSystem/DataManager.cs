using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManager : MonoBehaviour {

    public enum TableName
    {
        对话系统数据,
    }

    public static Dictionary<TableName, Table> allTables = new Dictionary<TableName, Table>();

    void Awake()
    {
        allTables.Add(TableName.对话系统数据, new Table(Application.dataPath + "/Config/TalkData/TalkTabel.txt"));
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

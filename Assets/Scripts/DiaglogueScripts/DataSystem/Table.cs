using UnityEngine;
using System.Collections;
using System.IO;

public class Table {

    string path;

    string[,] datas;

    public Table(string path)
    {
        this.path = path;
        Load();
    }

    void Load()
    {
        string[]  buff = File.ReadAllLines(path);

        //遍历行
        for (int i = 0; i < buff.Length; i++)
        { 
            string[] temp = buff[i].Split("\t"[0]);

            //初始化data 数组
            if (datas == null)
            { 
                datas = new string[buff.Length, temp.Length];
            }

            //遍历列
            for (int j = 0; j < temp.Length; j++)
            {
                datas[i, j] = temp[j];
            }
        }
    }

    public string[] GetLineWithID(string id)
    {
        for (int i = 0; i < datas.GetLength(0); i++)
        {
            string temp = datas[i, 0];

            if (temp == id)
            {
                string[] data = new string[datas.GetLength(1)];

                for (int j = 0; j < data.Length; j++)
                {
                    data[j] = datas[i, j];
                }

                return data;
            }
        }

        return null;
    }

    public string GetDataWithIDAndIndex(string id, int index)
    {
        string[] temp = GetLineWithID(id);

        if (temp != null)
        {
            if (temp.Length > index)
            {
                return temp[index];
            }
        }

        return null;
    }

    public string[] GetLineWithIndex(int index)
    {
        if (datas.GetLength(0) > index)
        {
            string[] data = new string[datas.GetLength(1)];

            for (int j = 0; j < data.Length; j++)
            {
                data[j] = datas[index, j];
            }

            return data;
        }

        return null;
    }


    public int GetlineCount()
    {
        return datas.GetLength(0);
    }

    public int GetColumCount()
    {
        return datas.GetLength(1);
    }
}

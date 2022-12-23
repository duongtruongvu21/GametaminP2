using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class LocalData
{
    public int Dolar;
    public Dictionary<int, int> MyItems;
    public DateTime LastViewAds;

    public LocalData()
    {
        Dolar = 0;
        MyItems = new Dictionary<int, int>();
        LastViewAds = DateTime.Now;
    }
}

public class SaveSystem
{
    public static void Save()
    {
        LocalData data = GameData.Instance.LocalData;
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data.yT";

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(stream, data);
            Debug.Log($"Save: {path}");
        }
    }

    public static LocalData Load()
    {
        LocalData data = new LocalData();
        string path = Application.persistentDataPath + "/data.yT";
        Debug.Log($"Load: {path}");

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                try
                {
                    data = (LocalData)formatter.Deserialize(stream);
                }
                catch (Exception)
                {
                    return new LocalData();
                }
            }
        }

        return data;
    }
}
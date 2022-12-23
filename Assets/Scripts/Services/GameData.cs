using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;
    public List<STO_ShopItem> ShopItems;
    LocalData localData;
    public LocalData LocalData
    {
        get
        {
            if (localData == null)
                localData = SaveSystem.Load();
            return localData;
        }
        set
        {
            localData = value;
        }
    }


    private void Awake()
    {
        Instance = this;
    }
}

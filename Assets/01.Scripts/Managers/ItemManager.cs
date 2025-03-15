using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>, ISingletonInitializer
{
    private ItemDB itemDB;

    public void Init()
    {
        itemDB = new ItemDB(DataManager.Instance._DataDB.GetItemList());
    }

}

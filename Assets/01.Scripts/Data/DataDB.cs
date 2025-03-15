using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDB
{
    private List<MonsterData> monsters = new List<MonsterData>();
    private List<ItemData> items = new List<ItemData>();

    public DataDB()
    {
        var res = ResourceManager.Instance.LoadResource<DataSheet>("DataSO/DataSheet");
        monsters = res.MonsterList;
        items = res.ItemList;
    }

    public List<MonsterData> GetMonsterList()
    {
        return monsters;
    }

    public List<ItemData> GetItemList()
    {
        return items;
    }
}

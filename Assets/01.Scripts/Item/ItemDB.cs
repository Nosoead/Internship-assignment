using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB
{
    private Dictionary<int, ItemData> items = new Dictionary<int, ItemData>();

    public int DataCount => items.Count;

    public ItemDB(List<ItemData> itemList)
    {
        int count = itemList.Count;
        for (int i = 0; i < count; i++)
        {
            var item = itemList[i];
            if (items.ContainsKey(item.itemID))
            {
                items[item.itemID] = item;
            }
            else
            {
                items.Add(item.itemID, item);
            }
        }
    }

    public ItemData Get(int id)
    {
        if (items.ContainsKey(id))
        {
            return items[id];
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemManager : Singleton<ItemManager>, ISingletonInitializer
{
    private ItemDB itemDB;
    private SpriteList spriteList;
    private Dictionary<int, Sprite> itemIcon = new Dictionary<int, Sprite>();
    private Dictionary<int, Sprite> itemSprite = new Dictionary<int, Sprite>();
    public void Init()
    {
        itemDB = new ItemDB(DataManager.Instance._DataDB.GetItemList());
        spriteList = ResourceManager.Instance.LoadResource<SpriteList>("GameObject/SpriteList");
        DictionarySetting();
    }
    
    private void DictionarySetting()
    {
        for (int i = 0; i < spriteList.key.Count; i++)
        {
            if (itemIcon.ContainsKey(spriteList.key[i]))
            {
                itemIcon[i] = spriteList.iconSpriteList[i];
            }
            else
            {
                itemIcon.Add(spriteList.key[i], spriteList.iconSpriteList[i]);
            }

            if (itemSprite.ContainsKey(spriteList.key[i]))
            {
                itemSprite[i] = spriteList.itemSpriteList[i];
            }
            else
            {
                itemSprite.Add(spriteList.key[i], spriteList.itemSpriteList[i]);
            }
        }
    }

    public ItemData GetItemDB(int id)
    {
        return itemDB.Get(id);
    }

    public Sprite GetIcon(int id)
    {
        return itemIcon[id];
    }

    public Sprite GetSprite(int id)
    {
        return itemSprite[id];
    }
}

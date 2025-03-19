using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledItem : MonoBehaviour, ISetPooledObject<PooledItem>, IInteractable
{
    private ItemData itemData;
    private Sprite itemSprite;
    private SpriteRenderer spriteRenderer;

    protected IObjectPool<PooledItem> objectPool;
    public IObjectPool<PooledItem> ObjectPool
    { get => objectPool; set => objectPool = value; }

    public void SetPooledObject(IObjectPool<PooledItem> pool)
    {
        ObjectPool = pool;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetItem(int id)
    {
        itemData = ItemManager.Instance.GetItemDB(id);
        itemSprite = ItemManager.Instance.GetIcon(id);
        spriteRenderer.sprite = itemSprite;
    }

    public int Interact()
    {
        objectPool.Release(this);
        return itemData.itemID;
    }
}
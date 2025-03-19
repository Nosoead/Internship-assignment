using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledBullet : MonoBehaviour, ISetPooledObject<PooledBullet>
{
    protected IObjectPool<PooledBullet> objectPool;
    public IObjectPool<PooledBullet> ObjectPool
    { get => objectPool; set => objectPool = value; }

    public void SetPooledObject(IObjectPool<PooledBullet> pool)
    {
        ObjectPool = pool;
    }
}

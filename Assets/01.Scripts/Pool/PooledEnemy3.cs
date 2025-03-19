using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledEnemy3 : MonoBehaviour, ISetPooledObject<PooledEnemy3>
{
    private MonsterStatHandler monsterStatHandler;
    protected IObjectPool<PooledEnemy3> objectPool;
    public IObjectPool<PooledEnemy3> ObjectPool
    { get => objectPool; set => objectPool = value; }

    public void SetPooledObject(IObjectPool<PooledEnemy3> pool)
    {
        ObjectPool = pool;
    }
    private void Awake()
    {
        monsterStatHandler = GetComponent<MonsterStatHandler>();
    }

    private void OnEnable()
    {
        monsterStatHandler.OnDieEvent += OnPoolRelease;
    }

    private void OnDisable()
    {
        monsterStatHandler.OnDieEvent -= OnPoolRelease;
    }

    private void OnPoolRelease()
    {
        Invoke(nameof(Release), 0.5f);
    }

    private void Release()
    {
        if (objectPool != null)
        {
            objectPool.Release(this);
        }
    }
}

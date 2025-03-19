using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledEnemy4 : MonoBehaviour, ISetPooledObject<PooledEnemy4>
{
    private MonsterStatHandler monsterStatHandler;
    protected IObjectPool<PooledEnemy4> objectPool;
    public IObjectPool<PooledEnemy4> ObjectPool
    { get => objectPool; set => objectPool = value; }

    public void SetPooledObject(IObjectPool<PooledEnemy4> pool)
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
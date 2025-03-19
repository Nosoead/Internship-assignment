using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class MonsterStatHandler : MonoBehaviour
{
    public UnityAction<string, float> OnSubscribeToStatUpdateEvent;
    public UnityAction<float> OnHealthDataToUIEvent;
    public UnityAction OnDieEvent;
    public string monsterID;

    private MonsterAnimationController animationController;
    private ParsedMonsterData monsterData;
    private MonsterStat stat;
    private StatCalculator calculator;

    private IObjectPool<PooledItem> itemPool;

    private void Awake()
    {
        if (monsterID == null) return;
        stat = new MonsterStat();
        calculator = new StatCalculator();
        animationController = GetComponent<MonsterAnimationController>();
    }


    private void OnEnable()
    {
        stat.OnStatUpdatedEvent += OnStatUpdatedEvent;
        stat.OnHealthUpdateEvent += OnHealthUpdateEvent;
        stat.OnDie += OnDie;
        if (monsterData != null)
        {
            stat.Init(monsterData);
        }
    }

    private void OnDisable()
    {
        stat.OnStatUpdatedEvent -= OnStatUpdatedEvent;
        stat.OnHealthUpdateEvent -= OnHealthUpdateEvent;
        stat.OnDie -= OnDie;
    }

    private void Start()
    {
        monsterData = EntityManager.Instance.GetMonsterDB(monsterID);
        stat.Init(monsterData);
        itemPool = PoolManager.Instance.GetObjectFromPool<PooledItem>(PoolType.PooledItem);
    }

    public ParsedMonsterData GetMonsterData()
    {
        return monsterData;
    }

    private void OnStatUpdatedEvent(string key, float value)
    {
        OnSubscribeToStatUpdateEvent?.Invoke(key, value);
    }

    private void OnHealthUpdateEvent(float currentHealth, float maxHealth)
    {
        float healthFillAmount = currentHealth / maxHealth;
        OnHealthDataToUIEvent?.Invoke(healthFillAmount);
    }

    public void ApplyDamage(float damage)
    {
        float result = calculator.Substract(stat.GetStatValue("CurrentHealth"), damage);
        animationController.OnHIt();
        stat.UpdateCurrentHealth(result);
    }

    private void OnDie()
    {
        animationController.OnDie();
        OnDieEvent?.Invoke();
        int id = RandomItem();
        if (id != 0)
        {
            PooledItem pooledItem = itemPool.Get();
            pooledItem.SetItem(id);
            pooledItem.transform.position = transform.position;
        }
    }

    private int RandomItem()
    {
        if (monsterData.dropItem.Length == 1 && monsterData.dropItem[0] != 0)
        {
            return monsterData.dropItem[0];
        }
        return 0;
    }
}

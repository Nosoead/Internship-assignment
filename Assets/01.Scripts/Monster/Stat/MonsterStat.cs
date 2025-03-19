using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterStat
{
    public UnityAction<string, float> OnStatUpdatedEvent;
    public UnityAction<float, float> OnHealthUpdateEvent;
    public UnityAction OnDie;

    private Dictionary<string, float> statDictionary;
    private ParsedMonsterData monsterData = new ParsedMonsterData();
    public void Init(ParsedMonsterData monsterData)
    {
        this.monsterData = monsterData;
        CreateStatDictionary(monsterData.attack, monsterData.maxHP, monsterData.moveSpeed);

        foreach (var stat in statDictionary)
        {
            OnStatUpdatedEvent?.Invoke(stat.Key, stat.Value);
        }
        OnHealthUpdateEvent?.Invoke(statDictionary["CurrentHealth"], statDictionary["MaxHealth"]);
    }

    private void CreateStatDictionary(float attack, float maxHealth, float moveSpeed)
    {
        statDictionary = new Dictionary<string, float>
        {
            { "Attack", attack },
            { "MaxHealth", maxHealth },
            { "CurrentHealth", maxHealth },
            { "MoveSpeed", moveSpeed }
        };
    }

    public float GetStatValue(string statKey)
    {
        if (statDictionary.TryGetValue(statKey, out var currentValue))
        {
            return currentValue;
        }
        else
        {
            return -1f;
        }
    }

    public void UpdateStat(string statKey, float currentValue)
    {
        if (statDictionary.ContainsKey(statKey))
        {
            statDictionary[statKey] = currentValue;
            OnStatUpdatedEvent?.Invoke(statKey, currentValue);
            if (statKey == "MaxHealth")
            {
                OnHealthUpdateEvent?.Invoke(statDictionary["CurrentHealth"], currentValue);
            }
        }
        else
        {
            return;
        }
    }

    public void UpdateCurrentHealth(float currentHealth)
    {
        if (statDictionary["CurrentHealth"] != 0)
        {
            if (statDictionary.ContainsKey("CurrentHealth"))
            {
                float newValue = Mathf.Clamp(currentHealth, 0f, statDictionary["MaxHealth"]);
                statDictionary["CurrentHealth"] = newValue;
                OnHealthUpdateEvent?.Invoke(newValue, statDictionary["MaxHealth"]);
                if (statDictionary["CurrentHealth"] == 0)
                {
                    OnDie?.Invoke();
                }
            }
        }
    }

}

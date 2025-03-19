using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObject/PlayerSO", order = 0)]
public class PlayerSO : ScriptableObject
{
    public Dictionary<string, float> playerStats = new Dictionary<string, float>()
    {
        {"MaxHealth", 0f},
        {"CurrentHealth", 0f},
        {"MoveSpeed", 0f},
        {"TakeDamageCooldownTime", 0f},
        {"AutoAttackSpeed", 0f},
        {"AutoAttackRange", 0f},
        {"PassiveAttackSpeed", 0f},
        {"PassiveAttackRange", 0f},
        {"AttackPower",0f}
    };

    public PlayerStatData playerStatData = new PlayerStatData();
    public SyncStat sync = new SyncStat();
}

[System.Serializable]
public class PlayerStatData
{
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;
    public float takeDamageCooldownTime;
    public float autoAttackSpeed;
    public float autoAttackRange;
    public float passiveAttackSpeed;
    public float passiveAttackRange;
    public float attackPower;
}

public class SyncStat
{
    public void SyncToDictionary(Dictionary<string, float> dictionary, PlayerStatData statData)
    {
        dictionary["MaxHealth"] = statData.maxHealth;
        dictionary["CurrentHealth"] = statData.currentHealth;
        dictionary["MoveSpeed"] = statData.moveSpeed;
        dictionary["TakeDamageCooldownTime"] = statData.takeDamageCooldownTime;
        dictionary["AutoAttackSpeed"] = statData.autoAttackSpeed;
        dictionary["AutoAttackRange"] = statData.autoAttackRange;
        dictionary["PassiveAttackSpeed"] = statData.passiveAttackSpeed;
        dictionary["PassiveAttackRange"] = statData.passiveAttackRange;
        dictionary["AttackPower"] = statData.attackPower;
    }

    public void SyncToPlayerStatData(Dictionary<string, float> dictionary, PlayerStatData statData)
    {
        statData.maxHealth = dictionary["MaxHealth"];
        statData.currentHealth = dictionary["CurrentHealth"];
        statData.moveSpeed = dictionary["MoveSpeed"];
        statData.takeDamageCooldownTime = dictionary["TakeDamageCooldownTime"];
        statData.autoAttackSpeed = dictionary["AutoAttackSpeed"];
        statData.autoAttackRange = dictionary["AutoAttackRange"];
        statData.passiveAttackSpeed = dictionary["PassiveAttackSpeed"];
        statData.passiveAttackRange = dictionary["PassiveAttackRange"];
        statData.attackPower = dictionary["AttackPower"];
    }
}
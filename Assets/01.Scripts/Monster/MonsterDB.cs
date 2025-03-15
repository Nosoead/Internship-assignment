using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class MonsterDB
{
    private Dictionary<string, ParsedMonsterData> monsters = new Dictionary<string, ParsedMonsterData>();

    public int DataCount => monsters.Count;

    public MonsterDB(List<MonsterData> monsterList)
    {
        int count = monsterList.Count;
        for (int i = 0; i < count; i++)
        {
            var monster = monsterList[i];
            var parsedMonster = ParseStringToIntArray(monster);
            if (monsters.ContainsKey(parsedMonster.monsterID))
            {
                monsters[parsedMonster.monsterID] = parsedMonster;
            }
            else
            {
                monsters.Add(parsedMonster.monsterID, parsedMonster);
            }
        }
    }

    private ParsedMonsterData ParseStringToIntArray(MonsterData monster)
    {
        ParsedMonsterData parsedMonster = new ParsedMonsterData();
        parsedMonster.monsterID = monster.monsterID;
        parsedMonster.name = monster.name;
        parsedMonster.description = monster.description;
        parsedMonster.attack = monster.attack;
        parsedMonster.attackMul = monster.attackMul;
        parsedMonster.maxHP = monster.maxHP;
        parsedMonster.maxHPMul = monster.maxHPMul;
        parsedMonster.attackRange = monster.attackRange;
        parsedMonster.attackRangeMul = monster.attackRangeMul;
        parsedMonster.attackSpeed = monster.attackSpeed;
        parsedMonster.moveSpeed = monster.moveSpeed;
        parsedMonster.minExp = monster.minExp;
        parsedMonster.maxExp = monster.maxExp;
        parsedMonster.dropItem = ParseDropItemString(monster.dropItem);

        return parsedMonster;
    }

    private int[] ParseDropItemString(string dropItem)
    {
        if (string.IsNullOrEmpty(dropItem))
        {
            return new int[0];
        }

        string[] splitItems = dropItem.Split(',');
        return Array.ConvertAll(splitItems, int.Parse); ;
    }

    public ParsedMonsterData Get(string id)
    {
        if (monsters.ContainsKey(id))
        {
            return monsters[id];
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class EntityManager : Singleton<EntityManager>, ISingletonInitializer
{
    private MonsterDB monsterDB;

    public void Init()
    {
        monsterDB = new MonsterDB(DataManager.Instance._DataDB.GetMonsterList());
    }
}

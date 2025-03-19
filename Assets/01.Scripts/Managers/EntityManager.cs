using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class EntityManager : Singleton<EntityManager>, ISingletonInitializer
{
    private PlayerSO playerData;
    private MonsterDB monsterDB;
    private Dictionary<string, ParsedMonsterData> monsterDictionary = new Dictionary<string, ParsedMonsterData>();
    private GameObject player;
    private IObjectPool<PooledEnemy0> enemy0;
    private IObjectPool<PooledEnemy1> enemy1;
    private IObjectPool<PooledEnemy2> enemy2;
    private IObjectPool<PooledEnemy3> enemy3;
    private IObjectPool<PooledEnemy4> enemy4;

    public void Init()
    {
        playerData = ResourceManager.Instance.LoadResource<PlayerSO>("DataSO/PlayerSO");
        monsterDB = new MonsterDB(DataManager.Instance._DataDB.GetMonsterList());
        GameObject playerPrefab = ResourceManager.Instance.LoadResource<GameObject>("GameObject/Player");
        if (playerPrefab == null)
        {
            return;
        }
        player = Instantiate(playerPrefab);
        player.name = playerPrefab.name;
        enemy0 = PoolManager.Instance.GetObjectFromPool<PooledEnemy0>(PoolType.PooledEnemy0);
        enemy1 = PoolManager.Instance.GetObjectFromPool<PooledEnemy1>(PoolType.PooledEnemy1);
        enemy2 = PoolManager.Instance.GetObjectFromPool<PooledEnemy2>(PoolType.PooledEnemy2);
        enemy3 = PoolManager.Instance.GetObjectFromPool<PooledEnemy3>(PoolType.PooledEnemy3);
        enemy4 = PoolManager.Instance.GetObjectFromPool<PooledEnemy4>(PoolType.PooledEnemy4);
    }

    public PlayerSO GetPlayerSO()
    {
        return playerData;
    }

    public ParsedMonsterData GetMonsterDB(string id)
    {
        return monsterDB.Get(id);
    }

    public Transform GetPlayerTransformData()
    {
        return player.transform;
    }

    public void SpawnRandomEnemy()
    {
        Vector3 spawnPosition = RandomPosition();

        int enemyType = Random.Range(0, 5); // 0~4 사이의 랜덤한 숫자 선택

        switch (enemyType)
        {
            case 0:
                if (enemy0.CountInactive == 0) return;
                enemy0.Get().transform.position = spawnPosition;
                break;
            case 1:
                if (enemy1.CountInactive == 0) return;
                enemy1.Get().transform.position = spawnPosition;
                break;
            case 2:
                if (enemy2.CountInactive == 0) return;
                enemy2.Get().transform.position = spawnPosition;
                break;
            case 3:
                if (enemy3.CountInactive == 0) return;
                enemy3.Get().transform.position = spawnPosition;
                break;
            case 4:
                if (enemy4.CountInactive == 0) return;
                enemy4.Get().transform.position = spawnPosition;
                break;
        }
    }

    private Vector3 RandomPosition()
    {
        Vector3 position = player.transform.position;
        float randomRadius = Random.Range(4f, 8f);
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float offsetX = Mathf.Cos(randomAngle) * randomRadius;
        float offsetY = Mathf.Sin(randomAngle) * randomRadius;
        return new Vector3(position.x + offsetX, position.y + offsetY, position.z);
    }

    public void AddDictionary(ParsedMonsterData monsterData)
    {
        if (monsterDictionary.ContainsKey(monsterData.monsterID))
        {
            return;
        }
        else
        {
            monsterDictionary.Add(monsterData.monsterID, monsterData);
        }
    }

    public Dictionary<string, ParsedMonsterData> GetDictionary()
    {
        return monsterDictionary;
    }
}

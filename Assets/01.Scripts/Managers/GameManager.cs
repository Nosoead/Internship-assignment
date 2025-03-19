using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>, ISingletonInitializer
{
    private GameObject map;
    private int stage;
    private float spawnInterval = 3f;
    private float spawnCount = 1f;
    private float intervalIncreaseTime = 10f;
    private bool isGameRunning = true;

    public void Init()
    {
        var obj = ResourceManager.Instance.LoadResource<GameObject>("GameObject/Grid");
        map = Instantiate(obj);
        MapParallax mapParallax = map.GetComponentInChildren<MapParallax>();
        mapParallax.InitParallax();
        UIManager.Instance.ToggleUI<DictionaryButtonUI>(true, true);
    }

    private void Start()
    {
        GameStart();
        Time.timeScale = 1.0f;
    }

    public void GameStart()
    {
        StartCoroutine(SpawnEnemiesOverTime());
        StartCoroutine(IncreaseSpawnRateOverTime());
    }

    public void GameEnd()
    {
        isGameRunning = false;
        StopCoroutine(SpawnEnemiesOverTime());
    }

    private IEnumerator SpawnEnemiesOverTime()
    {
        while (isGameRunning)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                EntityManager.Instance.SpawnRandomEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator IncreaseSpawnRateOverTime()
    {
        while (isGameRunning)
        {
            yield return new WaitForSeconds(intervalIncreaseTime);
            spawnCount++;
        }
    }
}
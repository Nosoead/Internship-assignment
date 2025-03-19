using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RuntimeManager
{
    private static readonly List<ISingletonInitializer> singletons = new List<ISingletonInitializer>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnSceneLoaded()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private static void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "testScene")
        {
            RegisterSingletonsInOrder();
            InitializeSingletons();
        }
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    public static void RegisterSingletonsInOrder()
    {
        singletons.Clear();
        singletons.Capacity = 6;

        singletons.Insert(0, DataManager.Instance);
        singletons.Insert(1, UIManager.Instance);
        singletons.Insert(2, GatherInputManager.Instance);
        singletons.Insert(3, ItemManager.Instance);
        singletons.Insert(4, EntityManager.Instance);
        singletons.Insert(5, PoolManager.Instance);
        singletons.Insert(6, GameManager.Instance);
    }

    private static void InitializeSingletons()
    {
        foreach (var singleton in singletons)
        {
            singleton.Init();
        }
    }
}
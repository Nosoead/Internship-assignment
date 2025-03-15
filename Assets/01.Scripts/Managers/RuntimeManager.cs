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
        Debug.Log($"씬 로드 완료: {scene.name}, 로드 모드: {mode}");

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
        singletons.Capacity = 4; // 리스트 크기 미리 설정 (예: 5개)

        singletons.Insert(0, DataManager.Instance);
        singletons.Insert(1, ItemManager.Instance);
        singletons.Insert(2, EntityManager.Instance);
        singletons.Insert(3, GatherInputManager.Instance);
    }

    private static void InitializeSingletons()
    {
        foreach (var singleton in singletons)
        {
            singleton.Init();
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPUI : MonoBehaviour
{
    [SerializeField] private MonsterStatHandler statHandler;
    [SerializeField] private Slider healthBar;

    private void Awake()
    {
        statHandler = GetComponentInParent<MonsterStatHandler>();
        healthBar = GetComponentInChildren<Slider>();

    }

    private void OnEnable()
    {
        statHandler.OnHealthDataToUIEvent += OnShowHealthbar;
    }

    private void OnDisable()
    {
        statHandler.OnHealthDataToUIEvent -= OnShowHealthbar;
    }

    private void OnShowHealthbar(float healthData)
    {
        healthBar.value = healthData;
    }
}

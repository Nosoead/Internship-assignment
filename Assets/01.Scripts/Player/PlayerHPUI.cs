using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : MonoBehaviour
{
    [SerializeField] private PlayerStatHandler statHandler;
    [SerializeField] private Slider healthBar;

    private void Awake()
    {
        statHandler = GetComponentInParent<PlayerStatHandler>();
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
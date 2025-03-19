using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStatHandler : MonoBehaviour
{
    public UnityAction<string, float> OnSubscribeToStatUpdateEvent;
    public UnityAction<float> OnHealthDataToUIEvent;
    public event UnityAction<float> OnHealEvent;

    private PlayerAnimationController animationController;
    private PlayerStat stat;
    private StatCalculator calculator;

    private void Awake()
    {
        animationController = GetComponent<PlayerAnimationController>();
        stat = new PlayerStat();
        calculator = new StatCalculator();
    }

    private void OnEnable()
    {
        stat.OnStatUpdatedEvent += OnStatUpdatedEvent;
        stat.OnHealthUpdateEvent += OnHealthUpdateEvent;
        stat.OnDie += OnDie;
    }

    private void OnDisable()
    {
        stat.OnStatUpdatedEvent -= OnStatUpdatedEvent;
        stat.OnHealthUpdateEvent -= OnHealthUpdateEvent;
        stat.OnDie -= OnDie;
    }

    private void Start()
    {
        stat.Init(EntityManager.Instance.GetPlayerSO());
    }

    private void OnStatUpdatedEvent(string key, float value)
    {
        OnSubscribeToStatUpdateEvent?.Invoke(key, value);
    }

    private void OnHealthUpdateEvent(float currentHealth, float maxHealth)
    {
        float healthFillAmount = currentHealth / maxHealth;
        OnHealthDataToUIEvent?.Invoke(healthFillAmount);
    }

    public void ApplyDamage(float damage)
    {
        float result = calculator.Substract(stat.GetStatValue("CurrentHealth"), damage);
        stat.UpdateCurrentHealth(result);
    }

    public void Heal(float heal)
    {
        OnHealEvent?.Invoke(heal);
        float result = calculator.Add(stat.GetStatValue("CurrentHealth"), heal, stat.GetStatValue("MaxHealth"));
        stat.UpdateCurrentHealth(result);
    }

    private void OnDie()
    {
        animationController.OnDie();
        GameManager.Instance.GameEnd();
        GatherInputManager.Instance.DisableInput();
    }

    public float GetStatValue(string key)
    {
        return stat.GetStatValue(key);
    }
}

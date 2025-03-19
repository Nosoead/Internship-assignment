using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamageable
{
    private PlayerStatHandler statHandler;
    private Coroutine coTakeDamageCoolDown;
    private WaitForSeconds takeDamageCooldownTime;
    private bool canTakeDamage = true;

    private void Awake()
    {
        statHandler = GetComponent<PlayerStatHandler>();
    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage) return;
        if(takeDamageCooldownTime == null)
            takeDamageCooldownTime = new WaitForSeconds(statHandler.GetStatValue("TakeDamageCooldownTime"));
        StartTakeDamageCooldown();
        statHandler.ApplyDamage(damage);
        
    }

    private void StartTakeDamageCooldown()
    {
        canTakeDamage = false;
        coTakeDamageCoolDown = StartCoroutine(CoolDownCoroutine());
    }

    private IEnumerator CoolDownCoroutine()
    {
        yield return takeDamageCooldownTime;
        canTakeDamage = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCondition : MonoBehaviour, IDamageable
{
    private MonsterStatHandler statHandler;
    private Coroutine coTakeDamageCoolDown;
    private WaitForSeconds takeDamageCooldownTime;
    private bool canTakeDamage = true;

    private void Awake()
    {
        statHandler = GetComponent<MonsterStatHandler>();
    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage) return;
        if (takeDamageCooldownTime == null)
            takeDamageCooldownTime = new WaitForSeconds(0.2f);
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

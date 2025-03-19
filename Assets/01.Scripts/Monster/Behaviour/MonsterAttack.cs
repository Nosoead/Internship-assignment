using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : Attack
{
    private MonsterStatHandler statHandler;
    private LayerMask playerLayer;
    private float attackPower;

    private void Awake()
    {
        statHandler = GetComponent<MonsterStatHandler>();
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnEnable()
    {
        statHandler.OnSubscribeToStatUpdateEvent += OnStatUpdatedEvent;
    }

    private void OnDisable()
    {
        statHandler.OnSubscribeToStatUpdateEvent -= OnStatUpdatedEvent;
    }

    private void OnStatUpdatedEvent(string statKey, float value)
    {
        switch (statKey)
        {
            case "Attack":
                attackPower = value;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            ApplyAttackLogic(collision.gameObject, attackPower);
        }
    }

    protected override void ApplyAttackLogic(GameObject target, float damage)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAutoAttack : Attack
{
    protected override void ApplyAttackLogic(GameObject target, float damage)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable !=null)
        {
            damageable.TakeDamage(damage);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

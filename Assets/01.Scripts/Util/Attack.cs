using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    protected abstract void ApplyAttackLogic(GameObject target, float damage);
}

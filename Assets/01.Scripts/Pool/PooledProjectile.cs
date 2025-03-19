using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledProjectile : MonoBehaviour, ISetPooledObject<PooledProjectile>
{
    private Rigidbody2D projectileRigidbody;
    private float attackRange;
    private float speed;
    private float damage;
    private bool isReleased = false;

    protected IObjectPool<PooledProjectile> objectPool;
    public IObjectPool<PooledProjectile> ObjectPool
    { get => objectPool; set => objectPool = value; }

    private void Awake()
    {
        projectileRigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetPooledObject(IObjectPool<PooledProjectile> pool)
    {
        ObjectPool = pool;
    }

    public void SetPosition(Vector3 playerPos, Vector3 targetPos)
    {
        float angle = Mathf.Atan2(targetPos.y- playerPos.y, targetPos.x - playerPos.x) * Mathf.Rad2Deg;
        transform.SetPositionAndRotation(playerPos, Quaternion.Euler(0, 0, angle-90f));
    }

    public void SetProjectileProperties(float attackRange, float speed, float damage)
    {
        isReleased = false;
        this.attackRange = attackRange;
        this.speed = speed;
        this.damage = damage;
    }

    public void Shoot(Vector3 targetDirection)
    {
        projectileRigidbody.velocity = targetDirection.normalized * speed;
    }

    public void Deactivate()
    {
        Invoke(nameof(DisappearProjectile), attackRange / speed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            if (isReleased) return;
            IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
            CancelInvoke(nameof(DisappearProjectile));
            ReleaseObject();
        }
    }

    private void DisappearProjectile()
    {
        if (isReleased) return;
        ReleaseObject();
    }

    private void ReleaseObject()
    {
        isReleased = true;
        projectileRigidbody.velocity = Vector2.zero;
        objectPool.Release(this);
    }
}

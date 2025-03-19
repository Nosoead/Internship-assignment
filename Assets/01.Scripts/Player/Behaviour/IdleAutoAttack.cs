using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class IdleAutoAttack : MonoBehaviour
{
    private PlayerStatHandler statHandler;
    private PlayerBehaviourHandler behaviourHandler;
    private Transform shootPoint;
    private Transform targetPoint;
    private IObjectPool<PooledProjectile> iProjectile;
    private float currentHealth;
    private float takeDamageCooldown;
    private float autoAttackSpeed;
    private float autoAttackRange;
    private float attackPower;
    private float timer;
    private LayerMask targetLayer;

    private void Awake()
    {
        shootPoint = GetComponentInChildren<Transform>();
        statHandler = GetComponentInParent<PlayerStatHandler>();
        behaviourHandler = GetComponentInParent<PlayerBehaviourHandler>();
        targetLayer = LayerMask.GetMask("Monster");
    }

    private void OnEnable()
    {
        statHandler.OnSubscribeToStatUpdateEvent += OnStatUpdateEvent;
    }

    private void OnDisable()
    {
        statHandler.OnSubscribeToStatUpdateEvent -= OnStatUpdateEvent;
    }

    private void Start()
    {
        iProjectile = PoolManager.Instance.GetObjectFromPool<PooledProjectile>(PoolType.PooledProjectile);
    }

    private void Update()
    {
        if (currentHealth == 0) return;
        if (!behaviourHandler.IsIdleState)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer > takeDamageCooldown)
        {
            timer = 0f;
            DetectEnemiesInRange(autoAttackRange);
            if (targetPoint == null) return;
            ShootProjectile();
        }
    }

    private void OnStatUpdateEvent(string key, float value)
    {
        switch (key)
        {
            case "CurrentHealth":
                currentHealth = value;
                break;
            case "TakeDamageCooldownTime":
                takeDamageCooldown = value;
                break;
            case "AutoAttackSpeed":
                autoAttackSpeed = value;
                break;
            case "AutoAttackRange":
                autoAttackRange = value;
                break;
            case "AttackPower":
                attackPower = value;
                break;
        }
    }

    public void DetectEnemiesInRange(float range)
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, range, targetLayer);
        Collider2D closestObject = null;
        float minDistance = Mathf.Infinity;
        foreach (Collider2D obj in detectedObjects)
        {
            float distance = Vector2.Distance(transform.position, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestObject = obj;
            }
        }
        if (closestObject != null)
        {
            targetPoint = closestObject.transform;
            if (closestObject.gameObject.TryGetComponent<MonsterStatHandler>(out var monsterStatHandler))
            {
                EntityManager.Instance.AddDictionary(monsterStatHandler.GetMonsterData());
            }
        }

        if (closestObject == null)
        {
            targetPoint = null;
            return;
        }

    }

    private void ShootProjectile()
    {
        PooledProjectile projectileObject = iProjectile.Get();
        if (projectileObject == null) return;
        projectileObject.SetPosition(shootPoint.position, targetPoint.position);
        projectileObject.SetProjectileProperties(autoAttackRange, autoAttackSpeed, attackPower);
        projectileObject.Shoot(targetPoint.position - transform.position);
        projectileObject.Deactivate();
    }
}

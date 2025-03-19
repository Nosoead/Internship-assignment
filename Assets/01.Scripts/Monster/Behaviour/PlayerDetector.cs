using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D monsterRigidbody;
    private float direction;

    private void Awake()
    {
        monsterRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        target = EntityManager.Instance.GetPlayerTransformData();
    }

    public void ApplyDetector(float speed)
    {
        if (target == null)
        {
            target = EntityManager.Instance.GetPlayerTransformData();
        }
        Vector2 targetPos = new Vector2(target.position.x, target.position.y);
        Vector2 direction = targetPos - monsterRigidbody.position;
        this.direction = direction.x;
        Vector2 moveVector = direction.normalized * speed * Time.fixedDeltaTime;
        monsterRigidbody.MovePosition(monsterRigidbody.position + moveVector);
        monsterRigidbody.velocity = Vector2.zero;
    }

    public float GetDirection()
    {
        return direction;
    }
}

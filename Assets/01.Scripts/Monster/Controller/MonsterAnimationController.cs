using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterAnimationController : MonoBehaviour
{
    private Animator animator;
    private MonsterController controller;
    private MonsterStatHandler statHandler;
    private SpriteRenderer sprite;
    private float moveDirection;

    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Dead = Animator.StringToHash("Dead");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<MonsterController>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        controller.OnDirectionEvent += OnDireiction;
    }

    private void OnDisable()
    {
        controller.OnDirectionEvent += OnDireiction;
    }

    private void LateUpdate()
    {
        SetSpriteFlip();
    }

    private void OnDireiction(float direction)
    {
        moveDirection = direction;
    }

    private void SetSpriteFlip()
    {
        if (moveDirection != 0)
        {
            sprite.flipX = moveDirection < 0;
        }
    }

    public void OnHIt()
    {
        animator.SetBool(Hit, true);
        Invoke(nameof(EndHit), 0.3f);
    }

    private void EndHit()
    {
        animator.SetBool(Hit, false);
    }

    public void OnDie()
    {
        animator.SetTrigger(Dead);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerController controller;
    private PlayerStatHandler statHandler;
    private SpriteRenderer sprite;
    private Vector2 moveDirection;

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Dead = Animator.StringToHash("Dead");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        controller.OnMoveEvent += OnMovement;
    }

    private void OnDisable()
    {
        controller.OnMoveEvent += OnMovement;
    }

    private void LateUpdate()
    {
        animator.SetFloat(Speed, moveDirection.magnitude);
        SetSpriteFlip();
  
    }

    private void OnMovement(Vector2 direction, bool isMoveKeyPressed)
    {
        moveDirection = direction;
    }


    private void SetSpriteFlip()
    {
        if (moveDirection.x != 0)
        {
            sprite.flipX = moveDirection.x < 0;
        }
    }

    public void OnDie()
    {
        animator.SetTrigger(Dead);
    }
}

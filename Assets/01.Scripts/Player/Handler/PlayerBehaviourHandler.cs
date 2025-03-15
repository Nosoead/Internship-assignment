using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourHandler : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    private Rigidbody2D playerRigidbody;
    private PlayerController controller;

    public Vector2 MoveDirection { get; private set; }
    public bool IsMoveKeyPressed { get; private set; }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
        StateMachine = new PlayerStateMachine(this);
    }

    private void OnEnable()
    {
        controller.OnMoveEvent += OnMovement;
    }

    private void OnDisable()
    {
        controller.OnMoveEvent -= OnMovement;
    }

    private void Start()
    {
        StateMachine.Initialize(StateMachine.idleState);
    }

    private void FixedUpdate()
    {
        StateMachine.Execute();
    }

    private void Update()
    {
        autoAttack();
    }

    private void OnMovement(Vector2 direction, bool isMoveKeyPressed)
    {
        MoveDirection = direction;
        IsMoveKeyPressed = isMoveKeyPressed;
    }

    private void autoAttack()
    {
        //자동공격하기 (무기 빙글빙글)
    }

    public void SetVelocity(Vector2 velocity)
    {
        playerRigidbody.velocity = velocity;
    }
}

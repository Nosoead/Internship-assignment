using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourHandler : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    private Rigidbody2D playerRigidbody;
    private PlayerController controller;
    private PlayerStatHandler statHandler;
    private IdleAutoAttack idleAutoAttack;

    public Vector2 MoveDirection { get; private set; }
    public float MoveSpeed { get; private set; }
    public float AutoAttackSpeed { get; private set; }
    public float AutoAttackRange { get; private set; }
    public bool IsMoveKeyPressed { get; private set; }
    public bool IsIdleState {  get; private set; }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
        statHandler = GetComponent<PlayerStatHandler>();
        StateMachine = new PlayerStateMachine(this);
        idleAutoAttack = GetComponentInChildren<IdleAutoAttack>();
    }

    private void OnEnable()
    {
        controller.OnMoveEvent += OnMovement;
        statHandler.OnSubscribeToStatUpdateEvent += OnStatUpdateEvent;
    }

    private void OnDisable()
    {
        controller.OnMoveEvent -= OnMovement;
        statHandler.OnSubscribeToStatUpdateEvent -= OnStatUpdateEvent;
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

    private void OnStatUpdateEvent(string key, float value)
    {
        switch (key)
        {
            case "MoveSpeed":
                MoveSpeed = value;
                break;
            case "autoAttackSpeed":
                AutoAttackSpeed = value;
                break;
            case "autoAttackRange":
                AutoAttackRange = value;
                break;
        }
    }

    private void autoAttack()
    {
        //자동공격하기 (무기 빙글빙글)
    }

    public void SetVelocity(Vector2 velocity)
    {
        playerRigidbody.velocity = velocity;
    }

    public void SetIsIdleState(bool isIdle)
    {
        IsIdleState = isIdle;
    }
}

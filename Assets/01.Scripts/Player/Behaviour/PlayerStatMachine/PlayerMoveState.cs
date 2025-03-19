using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : IState
{
    private PlayerBehaviourHandler player;

    public PlayerMoveState(PlayerBehaviourHandler player)
    {
        this.player = player;
    }

    public void Enter()
    {

    }

    public void Execute()
    {

        ApplyMovement();

        if (!player.IsMoveKeyPressed)
        {
            player.StateMachine.TransitionTo(player.StateMachine.idleState);
            return;
        }
    }

    public void Exit()
    {

    }

    private void ApplyMovement()
    {
        Vector2 velocity = player.MoveSpeed * player.MoveDirection;// * Time.fixedDeltaTime;
        player.SetVelocity(velocity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    private PlayerBehaviourHandler player;

    public PlayerIdleState(PlayerBehaviourHandler player)
    {
        this.player = player;
    }

    public void Enter()
    {
        
    }

    public void Execute()
    {
        if (player.IsMoveKeyPressed)
        {
            player.StateMachine.TransitionTo(player.StateMachine.moveState);
            return;
        }
    }

    public void Exit()
    {
        
    }
}

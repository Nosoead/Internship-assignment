using UnityEngine.InputSystem.XR.Haptics;

public class PlayerStateMachine
{
    public IState CurrentState { get; private set; }

    public PlayerIdleState idleState;
    public PlayerMoveState moveState;

    public PlayerStateMachine(PlayerBehaviourHandler player)
    {
        idleState = new PlayerIdleState(player);
        moveState = new PlayerMoveState(player);
    }

    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    public void Execute()
    {
        if (CurrentState != null)
        {
            CurrentState.Execute();
        }
    }
}

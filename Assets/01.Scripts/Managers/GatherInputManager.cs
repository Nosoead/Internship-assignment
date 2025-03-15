using UnityEngine;

public class GatherInputManager : Singleton<GatherInputManager>, ISingletonInitializer
{
    private PlayerInput input;

    protected override void Awake()
    {
        base.Awake();
    }
 
    public void Init()
    {
        input = new PlayerInput();
    }

    public PlayerInput GetPlayerInput()
    {
        return input;
    }

    public void EnableInput()
    {
        input.Player.Enable();
    }

    public void DisableInput()
    {
        input.Player.Disable();
    }

}

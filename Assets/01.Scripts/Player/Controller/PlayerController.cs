using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public UnityAction<Vector2, bool> OnMoveEvent;

    private PlayerInput input;

    private bool isMoveKeyPressed = false;

    private void Awake()
    {
        if (input == null)
        {
            //TODO Managers로 GatherInput먼저 넣고 GameManager에서 Player 끌고와서 셋팅하도록
            //input = GatherInputManager.Instance.GetPlayerInput();
            input = new PlayerInput();
        }
    }
    private void OnEnable()
    {
        input.Player.Move.started += PlayerMove;
        input.Player.Move.performed += PlayerMove;
        input.Player.Move.canceled += PlayerMove;
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
        //GatherInputManager.Instance.DisableInput();
    }

    public void PlayerMove(InputAction.CallbackContext context)
    {
        Vector2 moveDirection = context.ReadValue<Vector2>().normalized;
        if (context.canceled)
        {
            isMoveKeyPressed = false;
        }
        else if (context.started || context.performed)
        {
            isMoveKeyPressed = true;
        }
        OnMoveEvent?.Invoke(moveDirection, isMoveKeyPressed);
    }
}

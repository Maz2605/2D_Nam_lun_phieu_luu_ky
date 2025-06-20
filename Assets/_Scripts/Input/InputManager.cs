using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public bool IsInputEnabled { get; private set; } = true;

    public void EnableInput() => IsInputEnabled = true;
    public void DisableInput() => IsInputEnabled = false;
    public Vector2 RawInputMovement {get; private set;}
    public int NormInputX {get; private set;}
    public int NormInputY {get; private set;}
    
    public bool JumpInput {get; private set;}

    public bool ClickInput { get; private set; }

    public bool JumpInputStop { get; private set; }
    
    private float _inputHolderTime = 0.2f;

    private float _jumpInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (!IsInputEnabled) return;
        RawInputMovement = context.ReadValue<Vector2>();
        
        NormInputX = Mathf.RoundToInt(RawInputMovement.x);
        NormInputY = Mathf.RoundToInt(RawInputMovement.y);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (!IsInputEnabled) return;
        if(context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            _jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnClickInput(InputAction.CallbackContext context)
    {
        if (!IsInputEnabled) return;

        if (context.started)
        {
            ClickInput = true;
        }

        if (context.canceled)
        {
            ClickInput = false;
        }
    }
    public void SetJumpInputFalse() => JumpInput = false;
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + _inputHolderTime)
        {
            SetJumpInputFalse();
        }
    }
    
}

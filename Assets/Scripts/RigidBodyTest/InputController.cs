using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour, PlayerControls.IGamePlayActions
{
    PlayerControls controls;

    public bool isJumped;
    public Vector2 direction;

    public Vector2 rotate = Vector2.zero;
    public bool resetCam = false;


    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new PlayerControls();
            controls.GamePlay.SetCallbacks(this);
        }
        controls.GamePlay.Enable();
    }

    private void OnDisable()
    {
        controls.GamePlay.Disable();
    }

    public void OnGrow(InputAction.CallbackContext context)
    {
        isJumped = context.ReadValue<float>() > 0.0f;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        rotate = context.ReadValue<Vector2>();
    }

    public void OnRotateY(InputAction.CallbackContext context)
    {
    }

    public void OnResetCam(InputAction.CallbackContext context)
    {
        resetCam = context.ReadValue<float>() > 0.0f;
    }
}

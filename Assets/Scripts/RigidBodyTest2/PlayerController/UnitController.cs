using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitController : PlayerController
{

    public void OnMovement(InputAction.CallbackContext context)
    {
        player.Direction = context.ReadValue<Vector2>();
        //if (Mathf.Abs(direction.y) < 0.2f)
        //    direction.y = 0.0f;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        player.LookDir = context.ReadValue<Vector2>();

        if (gameCam.CamState != CameraController2.CamStates.Free
            && gameCam.CamState != CameraController2.CamStates.FirstPerson)
        {
            gameCam.SwitchFreeView();
        }
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        bool target = context.ReadValue<float>() > 0.01f;

        gameCam.SwitchTargetView(target);
    }


    private bool isFpsViewClicked = false;
    public void OnFirstPersonView(InputAction.CallbackContext context)
    {

        bool fpView = context.ReadValue<float>() >= 0.0f;

        if (fpView)
        {
            if (!isFpsViewClicked)
            {
                isFpsViewClicked = true;
                gameCam.SwitchFirstPersonView();
            }
        }

        if (context.ReadValue<float>() == 0.0f)
            isFpsViewClicked = false;
    }

    public void OnResetCamera(InputAction.CallbackContext context)
    {
        bool resetCam = context.ReadValue<float>() >= 0.0f;

        if (resetCam)
        {
            gameCam.ResetCameraState();
        }
    }

    private bool isJumpClicked = false;
    public void OnJump(InputAction.CallbackContext context)
    {
        player.JumpVal = context.ReadValue<float>();
        bool jump = player.JumpVal > 0.0f;

        if (jump && !isJumpClicked)
        {
            player.Jump();
            isJumpClicked = true;
        }
        Debug.Log(context);

        if (context.ReadValue<float>() == 0.0f)
            isJumpClicked = false;
    }

    private bool isNextPlayerClicked = false;
    public void OnNextPlayer(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.0f && !isNextPlayerClicked)
        {
            gameManager.SetNextPlayer();
            isNextPlayerClicked = true;
        }

        if (context.ReadValue<float>() == 0.0f)
            isNextPlayerClicked = false;
    }

    private bool isBackPlayerClicked = false;
    public void OnBackPlayer(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.0f && !isBackPlayerClicked)
        {
            gameManager.SetBackPlayer();
            isBackPlayerClicked = true;
        }

        if (context.ReadValue<float>() == 0.0f)
            isBackPlayerClicked = false;
    }

    private bool isCancelClicked = false;
    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.0f && !isCancelClicked)
        {
            Debug.Log("On Cancel");
            gameManager.SwapActionMap("Map");
            isCancelClicked = true;
        }

        if (context.ReadValue<float>() == 0.0f)
            isCancelClicked = false;
    }
}

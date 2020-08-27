using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : PlayerController
{ 

    public void OnMovement(InputAction.CallbackContext context)
    {
        cursor.Direction = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        cursor.LookDir = context.ReadValue<Vector2>();

        if (gameCam.CamState != CameraController2.CamStates.Free
            && gameCam.CamState != CameraController2.CamStates.FirstPerson)
        {
            gameCam.SwitchFreeView();
        }
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
            gameManager.SetPrevPlayer();
            isBackPlayerClicked = true;
        }
        
        if (context.ReadValue<float>() == 0.0f)
            isBackPlayerClicked = false;
    }

    private bool isSelectClicked = false;
    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.0f && !isSelectClicked)
        {
            Debug.Log("On Select");
            gameManager.SwapActionMap("Player");
            isSelectClicked = true;
        }


        if (context.ReadValue<float>() == 0.0f)
        {
            isSelectClicked = false;
            Debug.Log(context);
        }
    }

    private bool isCancelClicked = false;
    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.0f && !isCancelClicked)
        {
            Debug.Log("On Cancel");
            isCancelClicked = true;
        }

        if (context.ReadValue<float>() == 0.0f)
            isCancelClicked = false;
    }
}

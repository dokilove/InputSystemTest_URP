using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private List<TestPlayer2> players;
    private CameraController2 gameCam;

    [SerializeField]
    private int playerIndex = 0;

    public void SetPlayers(List<TestPlayer2> p, CameraController2 c)
    {
        players = p;
        gameCam = c;
        setCameraFollow();
    }

    private void SetNextPlayer()
    {
        playerIndex++;
        if (playerIndex >= players.Count)
            playerIndex = 0;

        setCameraFollow();
    }

    private void SetBackPlayer()
    {
        playerIndex--;
        if (playerIndex < 0)
            playerIndex = players.Count - 1;

        setCameraFollow();
    }

    private void setCameraFollow()
    {
        gameCam.SetFollow(players[playerIndex]);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        players[playerIndex].Direction = context.ReadValue<Vector2>();
        //if (Mathf.Abs(direction.y) < 0.2f)
        //    direction.y = 0.0f;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        players[playerIndex].LookDir = context.ReadValue<Vector2>();

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
        players[playerIndex].JumpVal = context.ReadValue<float>();
        bool jump = players[playerIndex].JumpVal > 0.0f;

        if (jump && !isJumpClicked)
        {
            players[playerIndex].Jump();
            isJumpClicked = true;
        }

        if (context.ReadValue<float>() == 0.0f)
            isJumpClicked = false;
    }

    private bool isNextPlayerClicked = false;
    public void OnNextPlayer(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.0f && !isNextPlayerClicked)
        {
            SetNextPlayer();
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
            SetBackPlayer();
            isBackPlayerClicked = true;
        }

        if (context.ReadValue<float>() == 0.0f)
            isBackPlayerClicked = false;
    }
}

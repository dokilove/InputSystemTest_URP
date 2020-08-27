using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    protected TestPlayer2 cursor;
    protected TestPlayer2 player;
    protected CameraController2 gameCam;
    protected GameManager gameManager;

    [SerializeField]
    PlayerInput playerInput;

    public void SetGameManager(GameManager mgr)
    {
        gameManager = mgr;
    }

    public void SetCam(CameraController2 c)
    {
        gameCam = c;
    }

    public void SetCursor(TestPlayer2 cs)
    {
        cursor = cs;
    }

    public void SetPlayer(TestPlayer2 p)
    {
        player = p;
    }

    public void SetCamFollow(TestPlayer2 follow)
    {
        if (null != gameCam)
            gameCam.SetFollow(follow);
    }

    #region Player
    private void OnMove(InputValue value)
    {
        player.Direction = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {

        player.LookDir = value.Get<Vector2>();

        if (gameCam.CamState != CameraController2.CamStates.Free
            && gameCam.CamState != CameraController2.CamStates.FirstPerson)
        {
            gameCam.SwitchFreeView();
        }
    }

    private void OnTarget(InputValue value)
    {
        gameCam.SwitchTargetView(value.isPressed);
    }

    private void OnFirstPersonView(InputValue value)
    {
        gameCam.SwitchFirstPersonView();
    }

    private void OnResetCamera(InputValue value)
    {
        gameCam.ResetCameraState();
    }

    private void OnJump(InputValue value)
    {
        player.JumpVal = value.isPressed;

        if (player.JumpVal)
        {
            player.Jump();
        }
    }

    private void OnNextPlayer(InputValue value)
    {
    }

    private void OnPrevPlayer(InputValue value)
    {
    }

    private void OnBackToMenu(InputValue value)
    {
        gameManager.SwapActionMap("Map");
        playerInput.SwitchCurrentActionMap("Map");
    }
    #endregion

    #region Map
    private void OnCursorMove(InputValue value)
    {
        cursor.Direction = value.Get<Vector2>();
    }

    private void OnCursorLook(InputValue value)
    {
        cursor.LookDir = value.Get<Vector2>();

        if (gameCam.CamState != CameraController2.CamStates.Free
            && gameCam.CamState != CameraController2.CamStates.FirstPerson)
        {
            gameCam.SwitchFreeView();
        }
    }

    private void OnNextUnit(InputValue value)
    {
        gameManager.SetNextPlayer();
    }

    private void OnPrevUnit(InputValue value)
    {
        gameManager.SetPrevPlayer();
    }

    private void OnSelectUnit(InputValue value)
    {
        gameManager.SwapActionMap("Player");
        playerInput.SwitchCurrentActionMap("Player");
    }
    #endregion
}

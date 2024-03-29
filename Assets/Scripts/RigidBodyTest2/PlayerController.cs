﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    protected TestPlayer2 player;
    protected Cursor cursor;
    protected CameraController2 gameCam;
    protected GameManager gameManager;

    [SerializeField]
    PlayerInput playerInput = null;

    public void ActivateInput(bool activate)
    {
        if (activate)
            playerInput.ActivateInput();
        else
            playerInput.DeactivateInput();
    }

    public void SetGameManager(GameManager mgr)
    {
        gameManager = mgr;
    }

    public void SetCam(CameraController2 c)
    {
        gameCam = c;
    }

    public void SetCursor(Cursor cs)
    {
        cursor = cs;
    }

    public void SetPlayer(TestPlayer2 p)
    {
        player = p;
    }
    
    public void SetCamFollow(ControlableUnit follow)
    {
        if (null != gameCam)
            gameCam.SetFollow(follow);
    }

    public void SwapActionMap(string mapName)
    {
        if (string.Compare(mapName, "Map") == 0)
        {
            SetCamFollow(cursor);
        }
        else if (string.Compare(mapName, "Player") == 0)
        {
            SetCamFollow(player);
        }
        playerInput.SwitchCurrentActionMap(mapName);
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
            gameCam.SetFreeView();
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
        SwapActionMap("Map");
        if (null != cursor)
        {
            cursor.SwitchState(Cursor.CursorState.Idle);
            cursor.SetChasePlayer();
        }
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

        //if (gameCam.CamState != CameraController2.CamStates.Free
        //    && gameCam.CamState != CameraController2.CamStates.FirstPerson)
        //{
        //    gameCam.SetFreeView();
        //}

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
        //gameManager.SwapActionMap("Player");
        //playerInput.SwitchCurrentActionMap("Player");
    }

    private void OnCursorJump(InputValue value)
    {
        if (null != cursor)
        {
            if (cursor.cursorState == Cursor.CursorState.Idle)
            {
                cursor.JumpVal = value.isPressed;

                if (cursor.JumpVal)
                {
                    cursor.Jump();
                }
            }
            else if (cursor.cursorState == Cursor.CursorState.Selectable)
            {
                SwapActionMap("Player");
                cursor.SelectPlayer();
                cursor.SetChasePlayer(player.transform);
            }
        }
    }
    #endregion
}

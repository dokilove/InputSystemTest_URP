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

    public void ControlChanged()
    {
        Debug.Log("Control Changed");
    }

}

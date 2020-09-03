using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CameraController2 gameCam = null;
    
    [SerializeField]
    PlayerController playerController = null;

    public PlayableUnitData[] playableUnits;
    public GameObject playerUnit;

    public List<TestPlayer2> players;
    public TestPlayer2 cursor;
    private int playerIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerController.SetGameManager(this);

        playerController.SetCam(gameCam);

        playerController.SetCursor(cursor);
        cursor.SetCam(gameCam);

        // 플레이어 스폰
        players = new List<TestPlayer2>();
        for(int i =0; i < playableUnits.Length; ++i)
        {
            GameObject go = Instantiate(playerUnit, playableUnits[i].startPos, Quaternion.identity) as GameObject;
            TestPlayer2 player = go.GetComponent<TestPlayer2>();
            go.name = playableUnits[i].playerId.ToString();
            player.Index = i;
            player.SetCam(gameCam);
            player.SetMat(playableUnits[i].mat);
            players.Add(player);            
        }


        //SetCurrentPlayer(players[playerIndex]);

        //currentController = cursorController;
        //currentController.SetCamFollow(cursor);

        playerController.SwapActionMap("Map");

        //playerInput.SwitchCurrentActionMap("Map");
    }
    
    public void SetNextPlayer()
    {
        playerIndex++;
        if (playerIndex >= players.Count)
            playerIndex = 0;

        SetCurrentPlayer(players[playerIndex]);
    }

    public void SetPrevPlayer()
    {
        playerIndex--;
        if (playerIndex < 0)
            playerIndex = players.Count - 1;

        SetCurrentPlayer(players[playerIndex]);
    }

    public void SelectCurrentPlayerWithID(int index)
    {
        playerIndex = index;
        SetCurrentPlayer(players[playerIndex]);
    }

    private void SetCurrentPlayer(TestPlayer2 player)
    {
        playerController.SetPlayer(player);
        cursor.transform.position = player.transform.position;
    }

}

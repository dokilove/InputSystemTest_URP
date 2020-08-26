using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    CursorController cursorController;
    [SerializeField]
    UnitController unitController;
    [SerializeField]
    private CameraController2 gameCam;

    [SerializeField]
    PlayerInput playerInput;    

    PlayerController currentController;

    public PlayableUnitData[] playableUnits;
    public GameObject playerUnit;

    public List<TestPlayer2> players;
    public TestPlayer2 cursor;
    private int playerIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        unitController.SetGameManager(this);
        cursorController.SetGameManager(this);

        unitController.SetCam(gameCam);
        cursorController.SetCam(gameCam);

        cursorController.SetCursor(cursor);
        cursor.SetCam(gameCam);

        // 플레이어 스폰
        players = new List<TestPlayer2>();
        for(int i =0; i < playableUnits.Length; ++i)
        {
            GameObject go = Instantiate(playerUnit, playableUnits[i].startPos, Quaternion.identity) as GameObject;
            TestPlayer2 player = go.GetComponent<TestPlayer2>();
            player.SetCam(gameCam);
            player.SetMat(playableUnits[i].mat);
            players.Add(player);            
        }


        currentController = unitController;
        SetCurrentPlayer(players[playerIndex]);

        //currentController = cursorController;
        //currentController.SetCamFollow(cursor);

        //SwapActionMap("Map");

        //playerInput.SwitchCurrentActionMap("Map");
    }

    public void SwapActionMap(string mapName)
    {
        Debug.Log("Current Action Map: " + playerInput.currentActionMap.ToString());
        playerInput.currentActionMap.Disable();
        Debug.Log("Current Action Map: " + playerInput.currentActionMap.ToString());

        //playerInput.SwitchCurrentActionMap(mapName);
        //Debug.Log("Current Action Map: " + playerInput.currentActionMap.ToString());

        ////playerInput.currentActionMap = playerInput.actions.FindActionMap(mapName);
        //playerInput.currentActionMap.Enable();

        StartCoroutine(SwapActionMapCoroutine(mapName));
    }


    IEnumerator SwapActionMapCoroutine(string mapName)
    {
        Debug.Log("Current Action Map: " + playerInput.currentActionMap.ToString());

        yield return new WaitForEndOfFrame();

        playerInput.SwitchCurrentActionMap(mapName);
        playerInput.currentActionMap.Enable();
        Debug.Log("Current Action Map: " + playerInput.currentActionMap.ToString());

    }

    public void SetNextPlayer()
    {
        playerIndex++;
        if (playerIndex >= players.Count)
            playerIndex = 0;

        SetCurrentPlayer(players[playerIndex]);
    }

    public void SetBackPlayer()
    {
        playerIndex--;
        if (playerIndex < 0)
            playerIndex = players.Count - 1;

        SetCurrentPlayer(players[playerIndex]);
    }

    private void SetCurrentPlayer(TestPlayer2 player)
    {
        currentController.SetPlayer(player);
        currentController.SetCamFollow(player);
        cursor.transform.position = player.transform.position;
    }

}

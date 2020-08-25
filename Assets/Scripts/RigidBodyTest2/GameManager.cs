using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    private CameraController2 gameCam;

    public PlayableUnitData[] playableUnits;
    public GameObject playerUnit;

    public List<TestPlayer2> players;

    // Start is called before the first frame update
    void Start()
    {
        players = new List<TestPlayer2>();
        for(int i =0; i < playableUnits.Length; ++i)
        {
            GameObject go = Instantiate(playerUnit, playableUnits[i].startPos, Quaternion.identity) as GameObject;
            TestPlayer2 player = go.GetComponent<TestPlayer2>();
            player.SetCam(gameCam);
            player.SetMat(playableUnits[i].mat);
            players.Add(player);            
        }

        playerController.SetPlayers(players, gameCam);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

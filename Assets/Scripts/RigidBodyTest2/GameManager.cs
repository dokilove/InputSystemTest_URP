using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using ProtoBuf;

public enum FileLoadMode
{
    Tsv,
    ProtoBuf,
    ScriptableObject,
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CameraController2 gameCam = null;
    
    [SerializeField]
    PlayerController playerController = null;

    [SerializeField]
    FileLoadMode mode = FileLoadMode.Tsv;

    //public PlayableUnitData[] playableUnits;
    public GameObject playerUnit;

    public List<TestPlayer2> players;
    public Cursor cursor;
    
    private int playerIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!ProtoBuf.Meta.RuntimeTypeModel.Default.IsDefined(typeof(Vector3)))
        {
            ProtoBuf.Meta.RuntimeTypeModel.Default.Add(typeof(Vector3), false).Add("x", "y", "z");
        }

        playerController.SetGameManager(this);

        playerController.SetCam(gameCam);

        playerController.SetCursor(cursor);
        cursor.SetCam(gameCam);

        // 플레이어 스폰

        if (mode == FileLoadMode.Tsv)
        {
            List<PlayerData> list = new List<PlayerData>();
            using (new CustomTimer("Parse tsv", 1))
            {
                string tsvPath = Path.Combine(Application.dataPath, "Data", "Tsv", "PlayerData.tsv");
                list = TsvParser.ParsePlayerData(tsvPath);

            }
            players = new List<TestPlayer2>();
            for (int i = 0; i < list.Count; ++i)
            {
                GameObject go = Instantiate(playerUnit, list[i].startPos, Quaternion.identity) as GameObject;
                TestPlayer2 player = go.GetComponent<TestPlayer2>();
                go.name = list[i].playerId.ToString();
                player.Index = i;
                player.SetCam(gameCam);
                player.SetMat(list[i].mat);
                players.Add(player);
            }
        }
        else if (mode == FileLoadMode.ProtoBuf)
        {
            List<PlayerDataProtoBuf> list = new List<PlayerDataProtoBuf>();
            using (new CustomTimer("Deserialize ProtoBuf", 1))
            {
                string filePath = Path.Combine(Application.dataPath, "Data", "ProtoBuf", "PlayerData.proto");
                list = Serializer.Deserialize<List<PlayerDataProtoBuf>>(new FileStream(filePath, FileMode.Open, FileAccess.Read));

            }
            players = new List<TestPlayer2>();
            for (int i = 0; i < list.Count; ++i)
            {
                GameObject go = Instantiate(playerUnit, list[i].startPos, Quaternion.identity) as GameObject;
                TestPlayer2 player = go.GetComponent<TestPlayer2>();
                go.name = list[i].playerId.ToString();
                player.Index = i;
                player.SetCam(gameCam);
                string matPath = Path.Combine("Materials", list[i].mat);
                Material mat = Resources.Load(matPath, typeof(Material)) as Material;
                player.SetMat(mat);
                players.Add(player);
            }
        }
        else if(mode == FileLoadMode.ScriptableObject)
        {
            PlayerDataCsv data = null;
            using (new CustomTimer("Scriptable Object", 1)){
                string resourcePath = Path.Combine("Data", "ScriptableObject", "PlayerData");
                data = Resources.Load(resourcePath, typeof(PlayerDataCsv)) as PlayerDataCsv;
            }

            players = new List<TestPlayer2>();
            for (int i = 0; i < data.items.Length; ++i)
            {
                Vector3 startPos = new Vector3(data.items[i].StartPos[0], data.items[i].StartPos[1], data.items[i].StartPos[2]);
                GameObject go = Instantiate(playerUnit, startPos, Quaternion.identity) as GameObject;
                TestPlayer2 player = go.GetComponent<TestPlayer2>();
                go.name = data.items[i].Id.ToString();
                player.Index = i;
                player.SetCam(gameCam);
                string matPath = Path.Combine("Materials", data.items[i].MatName);
                Material mat = Resources.Load(matPath, typeof(Material)) as Material;
                player.SetMat(mat);
                players.Add(player);
            }
        }


            //for(int i =0; i < playableUnits.Length; ++i)
            //{
            //    GameObject go = Instantiate(playerUnit, playableUnits[i].startPos, Quaternion.identity) as GameObject;
            //    TestPlayer2 player = go.GetComponent<TestPlayer2>();
            //    go.name = playableUnits[i].playerId.ToString();
            //    player.Index = i;
            //    player.SetCam(gameCam);
            //    player.SetMat(playableUnits[i].mat);
            //    players.Add(player);            
            //}


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
        playerController.ActivateInput(false);
        playerController.SetPlayer(player);
        cursor.transform.position = player.transform.position;
        playerController.ActivateInput(true);
    }

}

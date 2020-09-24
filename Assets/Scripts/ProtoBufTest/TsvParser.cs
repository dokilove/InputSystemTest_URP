using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TsvParser
{

    public static void Parsing(string path)
    {
        string[] allLines = File.ReadAllLines(path);

        // 첫줄은 넘긴다
        for (int i = 1; i < allLines.Length; ++i)
        {
            string[] splitData = allLines[i].Split('\t');           
  
            PlayableUnitData playableUnitData = ScriptableObject.CreateInstance<PlayableUnitData>();
            playableUnitData.playerId = int.Parse(splitData[0]);
            playableUnitData.actionPoint = int.Parse(splitData[1]);
            string matPath = Path.Combine("Materials", splitData[2]);
            Material mat = Resources.Load(matPath, typeof(Material)) as Material;
            playableUnitData.mat = mat;
            string[] positionData = splitData[3].Split(',');
            Vector3 startPos = new Vector3(float.Parse(positionData[0]), float.Parse(positionData[1]), float.Parse(positionData[2]));
            playableUnitData.startPos = startPos;

            string assetName = Path.Combine("Assets", "Data", "PlayableUnitData", "Player" + playableUnitData.playerId.ToString() + ".asset");
            AssetDatabase.CreateAsset(playableUnitData, assetName);
        }

        AssetDatabase.SaveAssets();
    }

    public static List<PlayerData> ParsePlayerData(string path)
    {
        string[] allLines = File.ReadAllLines(path);

        List<PlayerData> playerDataList = new List<PlayerData>();

        // 첫줄은 넘긴다
        for (int i = 1; i < allLines.Length; ++i)
        {
            string[] splitData = allLines[i].Split('\t');

            PlayerData playerData = new PlayerData();
            playerData.playerId = int.Parse(splitData[0]);
            playerData.actionPoint = int.Parse(splitData[1]);
            string matPath = Path.Combine("Materials", splitData[2]);
            Material mat = Resources.Load(matPath, typeof(Material)) as Material;
            playerData.mat = mat;
            string[] positionData = splitData[3].Split(',');
            Vector3 startPos = new Vector3(float.Parse(positionData[0]), float.Parse(positionData[1]), float.Parse(positionData[2]));
            playerData.startPos = startPos;

            playerDataList.Add(playerData);
        }

        return playerDataList;
    }

    public static List<PlayerDataProtoBuf> Tsv2ProtoBufPlayerData(string path)
    {
        string[] allLines = File.ReadAllLines(path);

        List<PlayerDataProtoBuf> playerDataList = new List<PlayerDataProtoBuf>();

        // 첫줄은 넘긴다
        for (int i = 1; i < allLines.Length; ++i)
        {
            string[] splitData = allLines[i].Split('\t');

            PlayerDataProtoBuf playerData = new PlayerDataProtoBuf();
            playerData.playerId = int.Parse(splitData[0]);
            playerData.actionPoint = int.Parse(splitData[1]);
            playerData.mat = splitData[2];
            string[] positionData = splitData[3].Split(',');
            Vector3 startPos = new Vector3(float.Parse(positionData[0]), float.Parse(positionData[1]), float.Parse(positionData[2]));
            playerData.startPos = startPos;

            playerDataList.Add(playerData);
        }

        return playerDataList;
    }
}

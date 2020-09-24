using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System.IO;


public class SaveAndLoadProtoBuf : MonoBehaviour
{
    public string FileName;
    public Persons MyGroup;
    string File1Path;

    private void Start()
    {
        if (!ProtoBuf.Meta.RuntimeTypeModel.Default.IsDefined(typeof(Vector3)))
        {
            ProtoBuf.Meta.RuntimeTypeModel.Default.Add(typeof(Vector3), false).Add("x", "y", "z");
        }

        File1Path = Path.Combine(Application.dataPath, "Data", FileName);
        Debug.Log(File1Path);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 200, 100), "Load File1"))
        {
            if (string.IsNullOrEmpty(File1Path))
            {
                Debug.LogError("Path is empty");
                return;
            }

            if (!File.Exists(File1Path))
            {
                Debug.LogError("file doesn't exist");
                return;
            }

            using (new CustomTimer("Deserialize Protobuf", 1))
            {
                MyGroup = Serializer.Deserialize<Persons>(new FileStream(File1Path, FileMode.Open, FileAccess.Read));
            }
        }


        if (GUI.Button(new Rect(10, 120, 200, 100), "Save File1"))
        {
            if (string.IsNullOrEmpty(File1Path))
            {
                Debug.LogError("Path is Empty");
                return;
            }

            if (MyGroup == null)
            {
                Debug.LogError("Value is null");
                return;
            }

            using (FileStream Stream = new FileStream(File1Path, FileMode.Create, FileAccess.Write))
            {
                Serializer.Serialize<Persons>(Stream, MyGroup);
                Stream.Flush();
            }
        }

        if (GUI.Button(new Rect(10, 230, 200, 100), "Tsv Parse"))
        {
           string tsvPath = Path.Combine(Application.dataPath, "Data", "Tsv", "PlayerData.tsv");
           TsvParser.Parsing(tsvPath);
        }


        if (GUI.Button(new Rect(10, 340, 200, 100), "Tsv to ProtoBuf"))
        {

            string tsvPath = Path.Combine(Application.dataPath, "Data", "Tsv", "PlayerData.tsv");
            List<PlayerDataProtoBuf> list = TsvParser.Tsv2ProtoBufPlayerData(tsvPath);

            if (list == null)
            {
                Debug.LogError("Value is null");
                return;
            }

            string filePath = Path.Combine(Application.dataPath, "Data", "ProtoBuf", "PlayerData.proto");

            using (FileStream Stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                Serializer.Serialize<List<PlayerDataProtoBuf>>(Stream, list);
                Stream.Flush();
            }
        }
    }
}

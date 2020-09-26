using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR

public class CSVImportPostprocessor : AssetPostprocessor
{
    public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {

            if (str.IndexOf("/PlayerData.csv") != -1)
            {
                TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                string assetfile = str.Replace(".csv", ".asset");
                PlayerDataCsv gm = AssetDatabase.LoadAssetAtPath<PlayerDataCsv>(assetfile);
                if (gm == null)
                {
                    gm = new PlayerDataCsv();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }

                gm.items = CSVSerializer.Deserialize<PlayerDataCsv.PlayerDataItem>(data.text);

                EditorUtility.SetDirty(gm);
                AssetDatabase.SaveAssets();
#if DEBUG_LOG || UNITY_EDITOR
                Debug.Log("Reimported Asset: " + str);
#endif
            }
        }
    }
}

#endif
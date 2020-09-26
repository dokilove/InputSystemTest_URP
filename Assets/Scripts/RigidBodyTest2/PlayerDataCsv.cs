using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDataCsv : ScriptableObject
{
    [Serializable]
    public class PlayerDataItem
    {
        public int Id;
        public int ActionPoint;
        public string MatName;
        public float[] StartPos;
    }

    public PlayerDataItem[] items;
    
}

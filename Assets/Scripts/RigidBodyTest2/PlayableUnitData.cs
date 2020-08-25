using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayableUnitData", menuName = "PlayableUnitData")]
public class PlayableUnitData : ScriptableObject
{
    public int playerId;
    public int actionPoint;
    public Material mat;

    public Vector3 startPos;
}


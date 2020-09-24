using UnityEngine;
using ProtoBuf;
using System;

public class PlayerData
{
    public int playerId;
    public int actionPoint;
    public Material mat;

    public Vector3 startPos;
}


[Serializable]
[ProtoContract]
public class PlayerDataProtoBuf
{
    [ProtoMember(1)]
    public int playerId;
    [ProtoMember(2)]
    public int actionPoint;
    [ProtoMember(3)]
    public string mat;

    [ProtoMember(4)]
    public Vector3 startPos;
}
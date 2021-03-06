﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System;

[Serializable]
[ProtoContract]
public class Persons
{
    [ProtoMember(1)]
    public string GroupName;
    [ProtoMember(2)]
    public Person[] GroupMember;
}

[Serializable]
[ProtoContract]
public class Person
{
    [ProtoMember(1)]
    public string name;

    [ProtoMember(2)]
    public int age;

    [ProtoMember(3)]
    public Vector3 position;
}

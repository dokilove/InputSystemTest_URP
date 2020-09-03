using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayer2 : ControlableUnit
{

    [SerializeField]
    private MeshRenderer[] meshRenderer = null;

    private int index;
    public int Index {
        get { return index; }
        set { index = value; }
    }

    public void SetMat(Material mat)
    {
        for (int i = 0; i < meshRenderer.Length; ++i)
        {
            meshRenderer[i].material = mat;
        }
    }

}

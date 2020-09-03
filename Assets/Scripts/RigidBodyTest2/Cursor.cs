using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : ControlableUnit
{
    [SerializeField]
    GameManager battleGameManager = null;

    [SerializeField]
    private Material idleMat = null;
    [SerializeField]
    private Material selectableMat = null;
    [SerializeField]
    private Material selectedMat = null;

    [SerializeField]
    private MeshRenderer[] meshRenderer = null;

    [SerializeField]
    private CursorSelectCollider selectCollider = null;

    public CursorState cursorState = CursorState.Idle;

    private int currentPlayerIndex = -1;

    public enum CursorState
    {
        Idle,
        Selectable,
        Selected,
    }

    public void SelectPlayer()
    {
        SwitchState(CursorState.Selected);
    }

    public void SetCurrentPlayerIndex(int index)
    {
        currentPlayerIndex = index;
        battleGameManager.SelectCurrentPlayerWithID(currentPlayerIndex);
    }

    public void SetMat(Material mat)
    {
        for (int i = 0; i < meshRenderer.Length; ++i)
        {
            meshRenderer[i].material = mat;
        }
    }
    
    public void SwitchState(CursorState state)
    {
        cursorState = state;
        switch (cursorState) {
            case CursorState.Idle:
                selectCollider.gameObject.SetActive(true);
                SetMat(idleMat);
                break;
            case CursorState.Selectable:
                SetMat(selectableMat);
                break;
            case CursorState.Selected:
                selectCollider.gameObject.SetActive(false);
                SetMat(selectedMat);
                break;
        }
    }
}

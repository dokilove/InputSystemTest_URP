using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    private Material idleMat;
    [SerializeField]
    private Material selectableMat;
    [SerializeField]
    private Material selectedMat;

    [SerializeField]
    private MeshRenderer[] renderer;

    [SerializeField]
    private CursorSelectCollider selectCollider;

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
        gameManager.SelectCurrentPlayerWithID(currentPlayerIndex);
    }

    public void SetMat(Material mat)
    {
        for (int i = 0; i < renderer.Length; ++i)
        {
            renderer[i].material = mat;
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

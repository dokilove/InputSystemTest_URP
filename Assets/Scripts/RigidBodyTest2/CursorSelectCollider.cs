using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSelectCollider : MonoBehaviour
{
    [SerializeField]
    private Cursor cursor = null;
       
    private void OnTriggerEnter(Collider other)
    {
        if (null != other && cursor.cursorState == Cursor.CursorState.Idle)
        {
            TestPlayer2 player = other.GetComponent<TestPlayer2>();
            if (null != player)
            {
                cursor.SetCurrentPlayerIndex(player.Index);
                cursor.SwitchState(Cursor.CursorState.Selectable);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (null != other)
        {
            cursor.SwitchState(Cursor.CursorState.Idle);
        }

    }
}

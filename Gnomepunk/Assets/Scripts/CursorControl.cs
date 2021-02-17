using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public CursorMode cursorMode = CursorMode.Auto;

    private GameCursor[] cursors;
    private int activeCursorIndex = -1;

    void Start()
    {
        cursors = GetComponents<GameCursor>();
        CycleState();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            CycleState();
        }
    }

    private void CycleState()
    {
        if (activeCursorIndex >= 0)
            cursors[activeCursorIndex].enabled = false;
        activeCursorIndex += 1;
        if (activeCursorIndex >= cursors.Length)
            activeCursorIndex = 0;
        cursors[activeCursorIndex].enabled = true;
        Cursor.SetCursor(cursors[activeCursorIndex].cursorTexture, Vector2.zero, cursorMode);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    enum CursorState
    { 
        MOVE,
        EXPLODE
    }

    public Texture2D moveCursorTexture;
    public Texture2D explodeCursorTexture;

    public CursorMode cursorMode = CursorMode.Auto;

    private Vector2 zero = Vector2.zero;

    private CursorState activeState;

    void Start()
    {
        activeState = CursorState.MOVE;
        Cursor.SetCursor(moveCursorTexture, zero, cursorMode);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //do something
        }
        if (Input.GetButtonDown("Fire2"))
        {
            cycleActiveState();
        }
    }

    private void cycleActiveState() 
    {
        switch (activeState)
        {
            case CursorState.MOVE:
                activeState = CursorState.EXPLODE;
                Cursor.SetCursor(explodeCursorTexture, zero, cursorMode);
                break;
            case CursorState.EXPLODE:
                activeState = CursorState.MOVE;
                Cursor.SetCursor(moveCursorTexture, zero, cursorMode);
                break;
            default:
                activeState = CursorState.MOVE;
                Cursor.SetCursor(moveCursorTexture, zero, cursorMode);
                break;
        }
    }
}

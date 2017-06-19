using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor_handler : MonoBehaviour {

    // deklaracje dot. własnego kursora
    public Texture2D cursorTexture;
    public CursorMode cursorMode;
    public Vector2 hotSpot;

    void Start()
    {
        // ustawienie własnego kursora
        cursorMode = CursorMode.Auto;
        hotSpot = Vector2.zero;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}

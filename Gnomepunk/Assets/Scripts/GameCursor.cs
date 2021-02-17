using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameCursor : MonoBehaviour
{
    public string modeName;
    public Texture2D cursorTexture;
    protected Camera cam;
    protected const float RAYCAST_DISTANCE = 100f;
    [SerializeField]
    protected LayerMask backplaneMask;
    [SerializeField]
    protected LayerMask interactsWithLayers;

    protected virtual void Start()
    {
        cam = Camera.main;
    }
}

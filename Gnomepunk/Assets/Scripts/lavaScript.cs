using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    public float lavaStartDelay = 10.0f;
    public float lavaRaisingSpeed = 0.1f;
    public bool isRaising = false;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        Invoke(nameof(StartLava), lavaStartDelay);
    }

    private void StartLava()
    {
        isRaising = true;
    }

    public void StopLava()
    {
        isRaising = false;
    }

    public void ResetLava()
    {
        transform.position = startPos;
    }

    void Update()
    {
        if (isRaising)
        {
            transform.position += Vector3.up * (lavaRaisingSpeed / 100);
        }
    }
}

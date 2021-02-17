using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaScript : MonoBehaviour
{
    public float lavaStartDelay = 10.0f;
    public float lavaRaisingSpeed = 0.1f;
    public bool isRaising = false;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = this.gameObject.transform.position;
        Invoke("startLava", lavaStartDelay);
    }

    private void startLava() {
        isRaising = true;
    }

    public void stopLava() {
        isRaising = false;
    }

    public void resetLava() {
        this.gameObject.transform.position = startPos;
    }

    void Update()
    {
        if (isRaising)
        {
            this.gameObject.transform.position += Vector3.up * (lavaRaisingSpeed/100);
        }
    }
}

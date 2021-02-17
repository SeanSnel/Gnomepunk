using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaScript : MonoBehaviour
{
    public float lavaStartDelay = 10.0f;
    public float lavaRaisingSpeed = 0.1f;
    public bool isRaising = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("startLava", lavaStartDelay);
    }

    private void startLava() {
        isRaising = true;
    }

    void Update()
    {
        if (isRaising)
        {
            this.gameObject.transform.position += Vector3.up * lavaRaisingSpeed;
        }
    }
}

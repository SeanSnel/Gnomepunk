using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public bool start = false;
    public float speed;
    public float rotation;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime * 20);
        }
    }

    public void StartRotation()
    {
        start = true;
    }

    public void StopRotation()
    {
        start = false;
    }
}

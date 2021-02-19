using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public bool start = false;
    public float speed = 0;
    public float rotationFactorX = 0;
    public float rotationFactorY = 0;
    public float rotationFactorZ = 0;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            transform.Rotate(new Vector3(speed * rotationFactorX, speed * rotationFactorY, speed * rotationFactorZ) * Time.deltaTime * 20);
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

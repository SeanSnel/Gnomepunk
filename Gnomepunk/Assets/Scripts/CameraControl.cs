using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float horizontalSpeed = 0.1f;
    public float verticalSpeed = 0.1f;
    public float animTime = 10f;

    private GameObject camera;
    private Vector3 targetPos;

    void Start()
    {
        camera = this.gameObject;
        targetPos = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos += new Vector3(Input.GetAxis("Horizontal") * horizontalSpeed, Input.GetAxis("Vertical") * verticalSpeed, 0);
        camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, (animTime * Time.deltaTime));
    }
}

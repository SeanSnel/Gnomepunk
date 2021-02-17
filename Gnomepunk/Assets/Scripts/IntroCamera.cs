using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    public float speed = 0.001f;
    public float distance = 10;
    public float fadeDuration = 4.0f;

    private GameObject cam;
    private Vector3 targetPos;
    private Vector3 beginPos;
    private bool fadeOut = false;
    private bool fadeIn = false;
    public GameObject plane;
    private float transparency = 0.0f;
    public float trancsparencyStep = 0.001f;
    private bool changing = false;

    void Start()
    {
        cam = this.gameObject;
        targetPos = cam.transform.position;
        beginPos = cam.transform.position;
        plane.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, transparency);
    }

    // Update is called once per frame
    void Update()
    {
        targetPos += new Vector3(speed, 0, 0);
        cam.transform.position = targetPos;
        if (transform.position.x + 1 >= distance && !changing)
        {
            StartCoroutine(handleReset());
        }

        if (fadeIn)
        {
            plane.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 0.0f, 0.0f, transparency);
            if (transparency > 0)
            {
                transparency -= trancsparencyStep;
            }            
        }

        if (fadeOut)
        { 
            plane.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 0.0f, 0.0f, transparency);
            transparency += trancsparencyStep;
        }
    }

    IEnumerator handleReset()
    {
        changing = true;
        fadeOut = true;

        yield return new WaitForSeconds(fadeDuration);
        transparency = 1.0f;
        cam.transform.position = beginPos;
        targetPos = beginPos;
        fadeOut = false;
        fadeIn = true;

        yield return new WaitForSeconds(fadeDuration);
        transparency = 0.0f;
        fadeIn = false;
        changing = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 10f;
    public float fadeDuration = 2.0f;

    private Vector3 beginPos;
    public GameObject plane;

    private Material material;

    void Start()
    {
        beginPos = transform.position;
        material = plane.GetComponent<MeshRenderer>().material;
        material.color = new Color(0, 0, 0, 1);
        StartCoroutine(nameof(ScreenPass));
    }

    IEnumerator ScreenPass()
    {
        while (true)
        {
            StartCoroutine(nameof(FadeIn));

            while (transform.position.x <= (beginPos.x + distance) - speed * fadeDuration)
            {
                transform.position += transform.right * speed * Time.deltaTime;
                yield return null;
            }

            StartCoroutine(nameof(FadeOut));

            float timer = 0;
            while (timer <= fadeDuration)
            {
                transform.position += transform.right * speed * Time.deltaTime;
                timer += Time.deltaTime;
                yield return null;
            }
            transform.position = beginPos;
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float timer = 0;
        while (material.color.a >= 0)
        {
            float alpha = Mathf.Lerp(1, 0, timer / fadeDuration);
            material.color = new Color(0, 0, 0, alpha);
            timer += Time.deltaTime;
            yield return null;
        }
        material.color = new Color(0, 0, 0, 0);
    }

    IEnumerator FadeOut()
    {
        float timer = 0;
        while (material.color.a <= 1f)
        {
            float alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            material.color = new Color(0, 0, 0, alpha);
            timer += Time.deltaTime;
            yield return null;
        }
        material.color = new Color(0, 0, 0, 1);
    }
}

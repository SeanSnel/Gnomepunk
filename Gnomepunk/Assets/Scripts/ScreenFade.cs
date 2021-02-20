using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScreenFade : MonoBehaviour
{

    public float fadeTime;
    public UnityEvent afterFadeOut;
    public UnityEvent afterFadeIn;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void FadeOut()
    {
        StartCoroutine(nameof(FadeOutRoutine));
    }

    public void FadeIn()
    {
        StartCoroutine(nameof(FadeInRoutine));
    }

    private IEnumerator FadeOutRoutine()
    {
        float timer = 0;
        while (image.color.a <= 1)
        {
            image.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, timer / fadeTime));
            timer += Time.deltaTime;
            yield return null;
        }
        image.color = new Color(0, 0, 0, 1);
        afterFadeOut.Invoke();
    }

    private IEnumerator FadeInRoutine()
    {
        float timer = 0;
        while (image.color.a >= 0)
        {
            image.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, timer / fadeTime));
            timer += Time.deltaTime;
            yield return null;
        }
        image.color = new Color(0, 0, 0, 1);
        afterFadeIn.Invoke();
    }

}

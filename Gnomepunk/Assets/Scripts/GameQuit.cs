using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameQuit : MonoBehaviour
{
    public float promptTime = 3f;
    private float timer;
    Text text;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        text.enabled = false;
        timer = promptTime + 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            StartCoroutine(PromptQuit());
    }

    IEnumerator PromptQuit()
    {
        text.enabled = true;
        float timer = 0;
        yield return null;
        while (timer <= promptTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
            else
                timer += Time.deltaTime;
            yield return null;
        }
        text.enabled = false;
    }
}

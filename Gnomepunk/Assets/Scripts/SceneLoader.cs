using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private float fadeTime;

    public void LoadScene(int scene)
    {
        LoadScene(SceneManager.GetSceneAt(scene).name);
    }

    public void LoadScene(string scene)
    {
        StartCoroutine(LoadSceneAsync(scene));
    }

    private IEnumerator LoadSceneAsync(string scene)
    {
        GameObject fadeObj = GameObject.FindGameObjectWithTag("FadeOut");
        Image fade = fadeObj.GetComponent<Image>();

        // Fade out
        float timer = 0;
        while (fade.color.a < 1)
        {
            fade.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, timer / fadeTime));
            timer += Time.deltaTime;
            yield return null;
        }

        // Load scene
        AsyncOperation loader = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        while (!loader.isDone)
        {
            yield return null;
        }

        timer = 0;
        // Fade in
        while (fade.color.a > 0)
        {
            fade.color = new Color(0, 0, 0, Mathf.Lerp(1, 0, timer / fadeTime));
            timer += Time.deltaTime;
            yield return null;
        }
    }
}

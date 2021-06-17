using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private float fadeTime;

    public int selectedLevel = 0;

    public void LoadScene(int scene)
    {
        LoadScene(SceneManager.GetSceneAt(scene).name);
        selectedLevel++;
    }

    public void LoadScene(string scene)
    {
        gameObject.SetActive(true);
        if (gameObject.activeInHierarchy == false) {
            switch (SceneManager.GetActiveScene().name)
            {
                case "level_tim":
                    GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneLoader>().LoadScene("Level_Robert");
                    break;
                case "Level_Robert":
                    GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneLoader>().LoadScene("level_tim2");
                    break;
                case "level_tim2":
                    GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneLoader>().LoadScene("EndScene");
                    break;
                default:
                    GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneLoader>().LoadScene("EndScene");
                    break;
            }
            
        }
        StartCoroutine(LoadSceneAsync(scene));
        selectedLevel++;
    }

    public void LoadNextLevel()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "level_tim":
                GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneLoader>().LoadScene("Level_Robert");
                break;
            case "Level_Robert":
                GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneLoader>().LoadScene("level_tim2");
                break;
            case "level_tim2":
                GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneLoader>().LoadScene("EndScene");
                break;
            default:
                GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneLoader>().LoadScene("EndScene");
                break;
        
    }
}

    public void ReloadScene() {
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().name));
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

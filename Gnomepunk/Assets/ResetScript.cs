using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour
{

    public GameObject sceneManager;

    void Update()
    {
        if (Input.GetButtonDown("Reset"))
        {
            Debug.Log("Reload");
            sceneManager.GetComponent<SceneLoader>().ReloadScene();
        }
    }
}

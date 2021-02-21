using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkipLevel : MonoBehaviour
{
    public UnityEvent onLevelSkip;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            onLevelSkip.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public int requiredAmount;
    public int currentAmount;

    public bool toggle = false;
    public bool isLevelSwitch = false;

    public UnityEvent enableTriggerEvent;
    public UnityEvent disableTriggerEvent;

    private bool isActivated = false;

    private GameObject plate;
    private Vector3 targetPosition;
    private float plateHeight = 0.15f;
    private int state = 0;

    void Start()
    {
        plate = transform.GetChild(0).gameObject;
        targetPosition = plate.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gnome") || other.gameObject.CompareTag("Box"))
        {
            currentAmount++;
            if (currentAmount <= requiredAmount)
            {
                targetPosition -= new Vector3(0, plateHeight / requiredAmount, 0);
                state++;
            }

            if (currentAmount >= requiredAmount && !isActivated)
            {
                isActivated = true;
                if (enableTriggerEvent == null || isLevelSwitch)
                {
                    Debug.Log("Unknown or empty trigger event");
                    GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneLoader>().LoadNextLevel();
                    return;
                }
                enableTriggerEvent.Invoke();
                Debug.Log("Did the thing");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.CompareTag("Gnome") || other.gameObject.CompareTag("Box")) && !toggle)
        {
            currentAmount--;

            if (currentAmount < requiredAmount && currentAmount >= 0)
            {
                targetPosition += new Vector3(0, plateHeight / requiredAmount, 0);
            }

            if (currentAmount < requiredAmount && isActivated)
            {
                isActivated = false;
                disableTriggerEvent.Invoke();
            }
        }
    }

    void Update()
    {
        if (plate != null)
        {
            plate.transform.position = Vector3.Lerp(plate.transform.position, targetPosition, 2 * Time.deltaTime);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public int requiredAmount;
    public int currentAmount;

    public bool toggle = false;

    public UnityEvent enableTriggerEvent;
    public UnityEvent disableTriggerEvent;

    private bool isActivated = false;

    private GameObject plate;
    private Vector3 targetPosition;
    private float plateHeight = 0.2f;
    private int state = 0;

    void Start()
    {
        plate = this.gameObject.transform.GetChild(0).gameObject;
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
                enableTriggerEvent.Invoke();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Gnome") || other.gameObject.CompareTag("Box") && !toggle)
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
        plate.transform.position = Vector3.Lerp(plate.transform.position, targetPosition, 2 * Time.deltaTime);
    }
}
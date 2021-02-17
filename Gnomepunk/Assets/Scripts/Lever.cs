using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    [SerializeField]
    UnityEvent triggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gnome"))
        {
            //Zorg dat gnome naar lever loopt o.i.d.


            triggerEvent.Invoke();
        }
    }
}

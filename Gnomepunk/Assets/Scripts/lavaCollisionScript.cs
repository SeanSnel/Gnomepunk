using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaCollisionScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gnome"))
        {
            Destroy(other.gameObject);
            //change to other.burn() when there are anims or effect there (could be interface)
        }
    }
}

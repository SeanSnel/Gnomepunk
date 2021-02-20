using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilator : MonoBehaviour
{
    // Hoe hoger de dampening hoe sneller de "collided iets" op de y as van de ventilator collider blijft hangen
    public float m_damping; 
    public float m_fanForce;
    private Collider m_Collider;
    
    void Start()
    {
        m_Collider = GetComponent<Collider>();
    }
    
    public void turnOn()
    {
        m_Collider.enabled = true;
    }

    public void turnOff()
    {
        m_Collider.enabled = false;
    }
    
    private void OnTriggerStay(Collider collided)
    {
        Vector3 velocity = collided.GetComponent<Rigidbody>().velocity;
        velocity += transform.up * m_fanForce * Time.deltaTime; 
        velocity -= velocity * m_damping * Time.deltaTime;
        collided.GetComponent<Rigidbody>().velocity = velocity;
    }

}

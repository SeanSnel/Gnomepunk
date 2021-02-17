using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilator : MonoBehaviour
{
    // Hoe hoger de dampening hoe sneller de "collided iets" op de y as van de ventilator collider blijft hangen
    float m_damping = 1f; 
    float m_fanForce = 20f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void OnTriggerStay(Collider _other)
    {
        Vector3 velocity =  _other.GetComponent<Rigidbody>().velocity;
        velocity += transform.up * m_fanForce * Time.deltaTime; 
        velocity -= velocity * m_damping * Time.deltaTime;
        _other.GetComponent<Rigidbody>().velocity = velocity;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GnomeMover : MonoBehaviour
{
    private Rigidbody rb;

    public float acceleration = 5;
    public float maxSpeed = 2;
    public float MaxSpeedSqr { get; private set; }
    public LayerMask gnomeMask = 0;
    public float overlapRadius;
    public float overlapFactor;

    [HideInInspector]
    public float xPos;
    [HideInInspector]
    public float yPos;

    [Header("--- DEBUG (DO NOT EDIT) ---")]
    // [HideInInspector]
    public float zPriority;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MaxSpeedSqr = Mathf.Pow(maxSpeed, 2);
        zPriority = Random.value - 0.5f;
    }

    public void RunAwayFrom(Vector3 position, float radius)
    {
        float distance = Vector3.Distance(rb.position, position);
        Vector3 pushDirection = (rb.position - position);
        pushDirection.y = 0;
        pushDirection.z = 0;
        pushDirection = pushDirection.normalized;
        Vector3 pushForce = pushDirection * acceleration * (-Mathf.Pow(distance.Remap(0, radius, 0, 1), 2) + 1);
        if (rb.velocity.sqrMagnitude < MaxSpeedSqr)
        {
            rb.AddForce(pushForce, ForceMode.Acceleration);
        }
    }

    void FixedUpdate()
    {
        Collider[] gnomes = Physics.OverlapSphere(transform.position, overlapRadius, gnomeMask);
        if (gnomes.Length > 0)
        {
            for (int i = 0; i < gnomes.Length; i++)
            {

            }
        }
        else
        {

        }

    }


}

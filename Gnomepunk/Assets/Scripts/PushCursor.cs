using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCursor : MonoBehaviour
{
    private Camera cam;
    private const float RAYCAST_DISTANCE = 100f;
    public LayerMask backplaneMask;
    public LayerMask gnomeMask;

    public float pushRadius = 3;

    public float MaxVelocitySqr { get; private set; }

    void Start()
    {
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, RAYCAST_DISTANCE, backplaneMask))
            {

                Collider[] gnomes = Physics.OverlapSphere(hit.point, pushRadius, gnomeMask);
                for (int i = 0; i < gnomes.Length; i++)
                {
                    gnomes[i].GetComponent<GnomeMover>().RunAwayFrom(hit.point, pushRadius);
                }
            }
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, RAYCAST_DISTANCE, backplaneMask))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(hit.point, pushRadius);
            }

        }

    }
#endif
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCursor : GameCursor
{
    public float pushRadius = 3;

    protected override void Start()
    {
        base.Start();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, RAYCAST_DISTANCE, interactsWithLayers))
            {

                Collider[] gnomes = Physics.OverlapSphere(hit.point, pushRadius, interactsWithLayers);
                for (int i = 0; i < gnomes.Length; i++)
                {
                    gnomes[i].GetComponentInParent<GnomeMover>().RunAwayFrom(hit.point, pushRadius);
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

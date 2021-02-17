using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnomes : MonoBehaviour
{
    GnomeMover[] gnomes;
    public Vector2 overlapBox;

    void Start()
    {
        gnomes = FindObjectsOfType<GnomeMover>();
    }

}

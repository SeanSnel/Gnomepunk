using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExplodable
{
    public void Explode(Vector3 position, float force, float radius);
}

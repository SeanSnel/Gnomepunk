using System.Collections;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private float[] liftHeights;
 
    private const float PRECISION = 0.02f;
    private int _nextHeightIndex = 0;

    public void MoveLift()
    {
        if (liftHeights.Length < _nextHeightIndex + 1)
        {
            //End of level
        }
        else
        {
            float nextHeight = liftHeights[_nextHeightIndex++];
            Vector3 newPosition = transform.position;
            newPosition.y = nextHeight;
            StartCoroutine(nameof(MoveLiftToLocation), newPosition);
        }
    }

    private IEnumerator MoveLiftToLocation(Vector3 targetLocation)
    {
        while (transform.position != targetLocation)
        {
            transform.position = Vector3.Lerp(transform.position, targetLocation, Time.deltaTime * movementSpeed);
            if (Vector3.Distance(transform.position, targetLocation) <= PRECISION)
            {
                transform.position = targetLocation;
            }
            yield return null;
        }
    }
}

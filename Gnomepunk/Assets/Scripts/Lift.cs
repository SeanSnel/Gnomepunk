using System.Collections;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private float movementTime = 3f;
    [SerializeField] private float[] liftHeights;

    private const float PRECISION = 0.02f;
    private int _nextHeightIndex = 0;

    private Rigidbody rb;

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

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

    public void moveDown()
    {
        if (liftHeights.Length < _nextHeightIndex)
        {
            float nextHeight = liftHeights[_nextHeightIndex--];
            Vector3 newPosition = transform.position;
            newPosition.y = nextHeight;
            StartCoroutine(nameof(MoveLiftToLocation), newPosition);
        }
    }

    private IEnumerator MoveLiftToLocation(Vector3 targetLocation)
    {
        float elapsedTime = 0;
        Vector3 startLocation = transform.position;
        while (elapsedTime <= movementTime)
        {
            elapsedTime += Time.deltaTime;
            float interpolationPoint = elapsedTime / movementTime;
            rb.MovePosition(Vector3.Lerp(startLocation, targetLocation, interpolationPoint));
            if (Vector3.Distance(transform.position, targetLocation) <= PRECISION)
            {
                rb.MovePosition(targetLocation);
            }

            yield return null;
        }
        rb.MovePosition(targetLocation);
    }
}
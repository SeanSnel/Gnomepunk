using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lift : MonoBehaviour
{
    [SerializeField] private float movementTime = 3f;
    [SerializeField] private float[] liftHeights;

    private const float PRECISION = 0.02f;
    public int _nextHeightIndex = 0;
    private Vector3 startPos;

    private Rigidbody rb;
    private bool canEnd = false;

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        startPos = transform.position;
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
        if (liftHeights.Length > -1)
        {
            Debug.Log("moving down");
            float nextHeight = transform.position.y;
            if (_nextHeightIndex > 0)
            {
                nextHeight = liftHeights[_nextHeightIndex--];
            }
            else if (_nextHeightIndex <= 0)
            {
                nextHeight = startPos.y;
            }
            
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

    public void UnlockEnd()
    {
        canEnd = true;
    }

    public void EnqueueSceneTransition(float afterSeconds)
    {
        StartCoroutine(TransitionScene(afterSeconds));
    }

    private IEnumerator TransitionScene(float afterSeconds)
    {
        yield return new WaitForSeconds(afterSeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
}
using System.Collections;
using UnityEngine;

public class BridgeControl : MonoBehaviour
{
    [Header("Rotation")] [SerializeField] private bool bridgeShouldRotate;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private Vector3 targetRotationVector = Vector3.zero;

    [Space(5)] [Header("Movement")] [SerializeField]
    private bool bridgeShouldMove;

    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private Vector3 moveDistanceVector = Vector3.zero;

    private Vector3 _startLocation;
    private Vector3 _startRotation;

    private void Start()
    {
        _startLocation = transform.position;
        _startRotation = transform.rotation.eulerAngles;
    }

    public void TriggerBridge()
    {
        if (bridgeShouldRotate)
        {
            RotateBridge(targetRotationVector);
        }

        if (bridgeShouldMove)
        {
            SlideOpen(moveDistanceVector);
        }
    }

    private void RotateBridge(Vector3 targetRotation)
    {
        StopCoroutine(nameof(RotateBridgeTo));
        StartCoroutine(nameof(RotateBridgeTo), targetRotation);
    }

    private IEnumerator RotateBridgeTo(Vector3 targetAngle)
    {
        Quaternion targetRotation = Quaternion.Euler(targetAngle);
        while (transform.rotation.eulerAngles != targetAngle)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }
    }

    private void SlideOpen(Vector3 movementVector)
    {
        StopCoroutine(nameof(MoveBridgeTo));
        Vector3 targetLocation = transform.position + movementVector;
        StartCoroutine(MoveBridgeTo(targetLocation));
    }

    private IEnumerator MoveBridgeTo(Vector3 targetLocation)
    {
        while (transform.position != targetLocation)
        {
            transform.position = Vector3.Lerp(transform.position, targetLocation, Time.deltaTime * movementSpeed);
            yield return null;
        }
    }

    public void RotateBack()
    {
        StopCoroutine(nameof(RotateBridgeTo));
        StartCoroutine(nameof(RotateBridgeTo), _startRotation);
    }

    public void MoveBack()
    {
        StopCoroutine(nameof(MoveBridgeTo));
        StartCoroutine(nameof(MoveBridgeTo), _startLocation);
    }
}
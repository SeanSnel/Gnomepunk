using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float horizontalSpeed = 0.1f;
    public float verticalSpeed = 0.1f;
    public float animTime = 10f;

    public float leftOffset;
    public GameObject leftBoundary;
    public float rightOffset;
    public GameObject rightBoundary;
    public float upperOffset;
    public GameObject upperBoundary;
    public float underOffset;
    public GameObject underBoundary;

    private GameObject _cam;
    private Vector3 _targetPos;

    private void Start()
    {
        _cam = this.gameObject;
        _targetPos = _cam.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 horizontalMovement = transform.right * (Input.GetAxis("Horizontal") * horizontalSpeed);
        Vector3 verticalMovement = transform.up * (Input.GetAxis("Vertical") * verticalSpeed);
        _targetPos = Vector3.ClampMagnitude(horizontalMovement + verticalMovement + transform.position, Mathf.Max(horizontalSpeed, verticalSpeed));
        CheckPosition();
        _cam.transform.position = Vector3.Lerp(_cam.transform.position, _targetPos, (animTime * Time.deltaTime));
    }

    private void CheckPosition() {
        if (upperBoundary != null)
        {
            if (_targetPos.y + upperOffset > upperBoundary.transform.position.y)
            {
                transform.position = new Vector3(_targetPos.x, upperBoundary.transform.position.y - upperOffset, _targetPos.z);
            }
        }
        if (underBoundary != null)
        {
            if (_targetPos.y - underOffset < underBoundary.transform.position.y)
            {
                _targetPos = new Vector3(_targetPos.x, underBoundary.transform.position.y + underOffset, _targetPos.z);
            }
        }
        if (leftBoundary != null)
        {
            if (_targetPos.x - leftOffset < leftBoundary.transform.position.x)
            {
                _targetPos = new Vector3(leftBoundary.transform.position.x + leftOffset, _targetPos.y, _targetPos.z);
            }
        }

        if (rightBoundary == null) return;
        if (_targetPos.x + rightOffset > rightBoundary.transform.position.x)
        {
            _targetPos = new Vector3(rightBoundary.transform.position.x - rightOffset, _targetPos.y, _targetPos.z);
        }
    }
}
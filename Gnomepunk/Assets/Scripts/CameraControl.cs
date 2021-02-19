using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraControl : MonoBehaviour
{
    public float horizontalSpeed = 0.1f;
    public float verticalSpeed = 0.1f;
    public float animTime = 10f;

    public float leftOffset;
    public GameObject leftBoundry;
    public float rightOffset;
    public GameObject rightBoundry;
    public float upperOffset;
    public GameObject upperBoundry;
    public float underOffset;
    public GameObject underBoundry;

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
        _targetPos = horizontalMovement + verticalMovement + transform.position;
        checkPosition();
        _cam.transform.position = Vector3.Lerp(_cam.transform.position, _targetPos, (animTime * Time.deltaTime));
    }

    private void checkPosition() {
        if (upperBoundry != null)
        {
            if (_targetPos.y + upperOffset > upperBoundry.transform.position.y)
            {
                transform.position = new Vector3(_targetPos.x, upperBoundry.transform.position.y - upperOffset, _targetPos.z);
            }
        }
        if (underBoundry != null)
        {
            if (_targetPos.y - underOffset < underBoundry.transform.position.y)
            {
                _targetPos = new Vector3(_targetPos.x, underBoundry.transform.position.y + underOffset, _targetPos.z);
            }
        }
        if (leftBoundry != null)
        {
            if (_targetPos.x - leftOffset < leftBoundry.transform.position.x)
            {
                _targetPos = new Vector3(leftBoundry.transform.position.x + leftOffset, _targetPos.y, _targetPos.z);
            }
        }
        if (rightBoundry != null)
        {
            if (_targetPos.x + rightOffset > rightBoundry.transform.position.x)
            {
                _targetPos = new Vector3(rightBoundry.transform.position.x - rightOffset, _targetPos.y, _targetPos.z);
            }
        }
    }
}
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraControl : MonoBehaviour
{
    public float horizontalSpeed = 0.1f;
    public float verticalSpeed = 0.1f;
    public float animTime = 10f;

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
        _cam.transform.position = Vector3.Lerp(_cam.transform.position, _targetPos, (animTime * Time.deltaTime));
    }
}
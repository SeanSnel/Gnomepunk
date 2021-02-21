using System.Collections;
using UnityEngine;

public class GnomeMover : MonoBehaviour, IExplodable
{
    [Tooltip("Acceleration to use when using the push cursor")]
    public float acceleration = 5;

    [Tooltip("Max walking speed when using the push cursor")]
    public float maxSpeed = 2;

    private float MaxSpeedSqr { get; set; }
    public LayerMask gnomeMask = 0;
    private float _startZPos;
    private Rigidbody _rb;
    private Quaternion _startRotation;
    private Animator _animator;
    private bool _grounded;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        MaxSpeedSqr = Mathf.Pow(maxSpeed, 2);
        _startZPos = transform.position.z;
        _startRotation = transform.rotation;
        _animator = GetComponentInChildren<Animator>();
        _grounded = true;
        _animator.SetBool("Grounded", _grounded);
    }

    private void Update()
    {
        Vector3 rbVelocity = _rb.velocity;
        Vector3 startEuler = _startRotation.eulerAngles;
        if (!_grounded) return;
        if (rbVelocity.x > 0.1)
        {
            transform.rotation = Quaternion.Euler(startEuler.x, startEuler.y - 90, startEuler.z);
            _animator.SetBool("Running", true);
        }
        else if (rbVelocity.x < -0.1)
        {
            transform.rotation = Quaternion.Euler(startEuler.x, startEuler.y + 90, startEuler.z);
            _animator.SetBool("Running", true);
        }
        else
        {
            transform.rotation = _startRotation;
            _animator.SetBool("Running", false);
        }
    }

    private void OnValidate()
    {
        MaxSpeedSqr = Mathf.Pow(maxSpeed, 2);
    }

    public void RunAwayFrom(Vector3 position, float radius)
    {
        float distance = Vector3.Distance(_rb.position, position);
        Vector3 pushDirection = (_rb.position - position);
        pushDirection.y = 0;
        pushDirection.z = 0;
        pushDirection = pushDirection.normalized;
        Vector3 pushForce = pushDirection * (acceleration * (-Mathf.Pow(distance.Remap(0, radius, 0, 1), 2) + 1));
        if (_rb.velocity.sqrMagnitude < MaxSpeedSqr)
        {
            _rb.AddForce(pushForce, ForceMode.Acceleration);
        }
    }

    public void Explode(Vector3 centerPosition, float centerForce, float radius)
    {
        UnlockRigidbody();
        _rb.AddExplosionForce(centerForce, centerPosition, radius, .5f, ForceMode.Impulse);
        _rb.velocity = new Vector3(_rb.velocity.x, _rb.velocity.y, 0);
        _grounded = false;
        _animator.SetBool("Grounded", _grounded);
        StopCoroutine(nameof(Recover));
        StartCoroutine(nameof(Recover));
    }

    private IEnumerator Recover()
    {
        do
        {
            yield return null;
        } while (!_rb.IsSleeping());

        yield return new WaitForSeconds(0.25f);
        _grounded = true;
        _animator.SetBool("Grounded", _grounded);
        transform.position = new Vector3(transform.position.x, transform.position.y, _startZPos);
        transform.rotation = _startRotation;
        LockRigidbody();
    }

    private void UnlockRigidbody()
    {
        _rb.constraints = RigidbodyConstraints.None;
    }

    private void LockRigidbody()
    {
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
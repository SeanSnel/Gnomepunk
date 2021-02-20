using UnityEngine;

public class MovingLava : MonoBehaviour
{
    [SerializeField] private float waviness = 0.1f;
    [SerializeField] private float moveSpeed = 0.01f;
    private Transform _lava0;
    private Transform _lava1;

    private Vector3 _lava0Start;
    private Vector3 _lava1Start;

    private void Start()
    {
        _lava0 = transform.GetChild(0);
        _lava1 = transform.GetChild(1);

        _lava0Start = _lava0.transform.localPosition;
        _lava1Start = _lava1.transform.localPosition; 
    }

    // Update is called once per frame
    private void Update()
    {
        _lava0.transform.localPosition = _lava0Start + new Vector3(0, Mathf.Sin(Time.time * moveSpeed) * waviness, 0);
        _lava1.transform.localPosition = _lava1Start - new Vector3(0, Mathf.Sin(Time.time * moveSpeed) * waviness, 0);
    }
}

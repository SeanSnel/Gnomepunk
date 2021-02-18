using System.Collections;
using UnityEngine;

public class MainMenuButtonAnimation : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10;
    private RectTransform _buttonImageTransform;

    private void Start()
    {
        _buttonImageTransform = GetComponent<RectTransform>();
        StartCoroutine(nameof(MoveButton));
    }

    private IEnumerator MoveButton()
    {
        while (_buttonImageTransform.localPosition.y > 0)
        {
            _buttonImageTransform.localPosition += Vector3.down;
            yield return null;
        }
    }
}
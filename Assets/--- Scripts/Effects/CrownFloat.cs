using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CrownFloat : MonoBehaviour
{
    [SerializeField] private float _floatHeight = 0.5f;
    [SerializeField] private float _floatSpeed = 1f; 
    [SerializeField] private float _rotationAngle = 30f; 
    [SerializeField] private float _rotationDuration = 0.25f;
    [SerializeField] private GameObject _crown;

    private Vector3 _initialPosition;

    private void Start()
    {
        StartCoroutine(WaitBeforeFloating());
    }

    IEnumerator WaitBeforeFloating()
    {
        yield return new WaitForSeconds(2.5f);
        
        _initialPosition = _crown.transform.position;
        FloatUpAndDown();
        RotateLeft();
    }

    private void FloatUpAndDown()
    {
        _crown.transform.DOMoveY(_initialPosition.y + _floatHeight, _floatSpeed).SetEase(Ease.InOutSine)
            .OnComplete(() => _crown.transform.DOMoveY(_initialPosition.y - _floatHeight, _floatSpeed)
                .SetEase(Ease.InOutSine).OnComplete(FloatUpAndDown));
    }

    private void RotateLeft()
    {
        _crown.transform.DORotate(new Vector3(0, 0, _rotationAngle), _rotationDuration).SetEase(Ease.Linear).OnComplete(RotateRight);
    }

    private void RotateRight()
    {
        _crown.transform.DORotate(new Vector3(0, 0, -_rotationAngle), _rotationDuration).SetEase(Ease.Linear).OnComplete(RotateLeft);
    }
}
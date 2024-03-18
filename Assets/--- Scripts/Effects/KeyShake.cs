using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class KeyShake : MonoBehaviour
{
    [SerializeField] private float _rotationAngle = 30f;
    [SerializeField] private float _rotationDuration = 0.25f;
    [SerializeField] private int _rotationCount = 2;
    [SerializeField] private float _delayBetweenRotations = 0.5f;
    [SerializeField] private GameObject _key;

    private int _currentRotaNb;

    private void Start()
    {
        _currentRotaNb = _rotationCount;
        StartCoroutine(WaitBeforeAnim());
    }

    IEnumerator WaitBeforeAnim()
    {
        yield return new WaitForSeconds(2f);
        RotateLeft();
    }

    private void RotateLeft()
    {
        _key.transform.DORotate(new Vector3(0, 0, _rotationAngle), _rotationDuration)
            .OnComplete(RotateRightWithDelay);
    }

    private void RotateRightWithDelay()
    {
        _key.transform.DORotate(new Vector3(0, 0, -_rotationAngle), _rotationDuration)
            .OnComplete(() =>
            {
                _currentRotaNb--;
                if (_currentRotaNb > 0)
                {
                    RotateLeft();
                }
                else
                {
                    _key.transform.DORotate(Vector3.zero, _rotationDuration)
                        .OnComplete(() => Invoke(nameof(RotateLeftWithDelay), _delayBetweenRotations));
                    _currentRotaNb = _rotationCount;
                }
            });
    }

    private void RotateLeftWithDelay()
    {
        _key.transform.DORotate(new Vector3(0, 0, _rotationAngle), _rotationDuration)
            .OnComplete(RotateRightWithDelay);
    }
}
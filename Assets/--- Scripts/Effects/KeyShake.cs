using UnityEngine;
using DG.Tweening;

public class KeyShake : MonoBehaviour
{
    public float rotationAngle = 30f;
    public float rotationDuration = 0.25f;
    public int rotationCount = 2;
    public float delayBetweenRotations = 0.5f; // Ajout d'un délai entre chaque rotation

    [SerializeField] private GameObject _key;

    private int _currentRotaNb;
    
    private void Start()
    {
        _currentRotaNb = rotationCount;
        RotateLeft();
    }

    private void RotateLeft()
    {
        _key.transform.DORotate(new Vector3(0, 0, rotationAngle), rotationDuration)
            .OnComplete(RotateRightWithDelay);
    }

    private void RotateRightWithDelay()
    {
        _key.transform.DORotate(new Vector3(0, 0, -rotationAngle), rotationDuration)
            .OnComplete(() => {
                _currentRotaNb--;
                if (_currentRotaNb > 0)
                {
                    RotateLeft();
                }
                else
                {
                    _key.transform.DORotate(Vector3.zero, rotationDuration)
                        .OnComplete(() => Invoke(nameof(RotateLeftWithDelay), delayBetweenRotations));
                    _currentRotaNb = rotationCount;
                }
            });
    }

    private void RotateLeftWithDelay()
    {
        _key.transform.DORotate(new Vector3(0, 0, rotationAngle), rotationDuration)
            .OnComplete(RotateRightWithDelay);
    }
}
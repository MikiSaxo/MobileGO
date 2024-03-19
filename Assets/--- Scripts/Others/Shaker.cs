using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shaker : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _durationShaking = 0f;

    public static Shaker Instance;
    private Vector3 _startPos;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        PlayerManager.Instance.PlayerHasSwipe += GoShake;
        _startPos = transform.position;
    }

    public void GoShakeAddPower(float addTime, float strengthAdded)
    {
        StartCoroutine(Shaking(addTime, strengthAdded));
    }

    private void GoShake()
    {
        StartCoroutine(Shaking(0,1));
    }

    private IEnumerator Shaking(float timeAdded, float strengthAdded)
    {
        float elapsedTime = 0f;

        var _addDuration = timeAdded + _durationShaking;

        while (elapsedTime < _addDuration)
        {
            elapsedTime += Time.deltaTime;
            float strength = _curve.Evaluate(elapsedTime / _addDuration) * strengthAdded;
            transform.position = _startPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = _startPos;
    }
    
    private void OnDisable()
    {
        PlayerManager.Instance.PlayerHasSwipe -= GoShake;
    }
}
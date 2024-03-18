using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class TitleAnim : MonoBehaviour
{
    [Header("--- Light ---")]
    [SerializeField] private GameObject _lightEffect;
    [SerializeField] private Transform[] _lightPoints;
    [SerializeField] private float _timeLightEffect = .5f;
    [SerializeField] private float _waitLight = 1.5f;
    
    [Header("--- Bounce ---")]
    [SerializeField] private float _punchScaleAmount = 0.2f;
    [SerializeField] private float _punchDuration = 0.2f; 
    [SerializeField] private float _pauseDuration = 1f;
    [SerializeField]  private RectTransform _rectTransform;

    private void Start()
    {
        GoEnd();
        Bounce();
    }

    private void GoEnd()
    {
        _lightEffect.transform.DOMove(_lightPoints[1].position, _timeLightEffect).SetEase(Ease.InExpo).OnComplete(TpStart);
    }

    private void TpStart()
    {
        _lightEffect.transform.DOMove(_lightPoints[0].position, 0);
        _lightEffect.transform.DOMove(_lightPoints[0].position, _waitLight).OnComplete(GoEnd);
    }

    private void Bounce()
    {
        _rectTransform.DOPunchScale(Vector3.one * _punchScaleAmount, _punchDuration, 5, 1).OnComplete(() =>
            {
                DOVirtual.DelayedCall(_pauseDuration, Bounce);
            });
    }
}

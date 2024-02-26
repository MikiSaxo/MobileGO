using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Spike : Module
{
    [Header("--- Spike ---")]
    [SerializeField] private Image _spikeImg;

    private int _count = 0;

    private void Start()
    {
        PlayerManager.Instance.PlayerHasSwipe += ChangeState;
    }

    private void ChangeState()
    {
        _count++;
        _spikeImg.gameObject.transform.DOComplete();
        
        if (_count > 1)
        {
            _count = 0;
            IsDeathModule = true;

            _spikeImg.gameObject.transform.DOScale(Vector3.one, .5f);
        }
        else
        {
            IsDeathModule = false;
            _spikeImg.gameObject.transform.DOScale(Vector3.one*.5f, .5f);
        }
    }

    private void OnDisable()
    {
        PlayerManager.Instance.PlayerHasSwipe -= ChangeState;
    }
}
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class BlinkAnim : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _texts;
    [SerializeField] private float _blinkDuration = 1.5f;

    void Start()
    {
        BlinkIn();
    }

    void BlinkIn()
    {
        for (int i = 0; i < _texts.Length; i++)
        {
            if(i == 0)
                _texts[i].DOFade(0f, _blinkDuration / 2).SetEase(Ease.InOutQuad).OnComplete(BlinkOut);
            else
                _texts[i].DOFade(0f, _blinkDuration / 2).SetEase(Ease.InOutQuad);
        }
    }

    void BlinkOut()
    {
        for (int i = 0; i < _texts.Length; i++)
        {
            if(i == 0)
                _texts[i].DOFade(1, _blinkDuration / 2).SetEase(Ease.InOutQuad).OnComplete(BlinkIn);
            else
                _texts[i].DOFade(1, _blinkDuration / 2).SetEase(Ease.InOutQuad);
        }
    }
}
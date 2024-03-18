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
        Blink();
    }

    void Blink()
    {
        foreach (var text in _texts)
        {
            text.DOFade(0f, _blinkDuration / 2).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                text.DOFade(1f, _blinkDuration / 2).SetEase(Ease.InOutQuad).OnComplete(Blink);
            });
        }
    }
}
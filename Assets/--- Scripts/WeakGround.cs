using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeakGround : Module
{
    [Header("--- Weak Ground ---")]
    [SerializeField] private Image _groundImg;
    [SerializeField] private Sprite _brokenGround;

    public bool IsWeakModule { get; set; }

    public void GoBroken()
    {
        _groundImg.sprite = _brokenGround;
        IsWeakModule = true;
    }
}
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Key : Module
{
    [Header("--- Key ---")] 
    [SerializeField] private Image _keyImg;
    [SerializeField] private Door _door;
    
    
    public bool HasGetKey { get; private set; }
    
    public void GetKey()
    {
        HasGetKey = true;
        _keyImg.DOFade(0, 1f);
        _door.OpenDoor();
    }
}

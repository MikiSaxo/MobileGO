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


    private bool _hasGetKey;
    
    public void GetKey()
    {
        if(_hasGetKey) return;
        
        _hasGetKey = true;
        _keyImg.DOFade(0, 1f);
        _door.OpenDoor();
    }

    public override void OnPlayerEnter()
    {
        GetKey();
    }
}

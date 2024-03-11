using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Key : ModuleFeature
{
    [Header("--- Key ---")] 
    [SerializeField] private Image _keyImg;
    [SerializeField] private Door _door;


    private bool _hasGetKey;

    private void GetKey()
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

    protected override void ResetStartPos()
    {
        _hasGetKey = false;
        _keyImg.DOFade(1, .5f);
        _door.CloseDoor();
    }
    
    private void OnDisable()
    {
        PlayerManager.Instance.PlayerIsDead -= ResetStartPos;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Key : ModuleFeature
{
    [Header("--- Key ---")] 
    [SerializeField] private SpriteRenderer _spriteKey;
    [SerializeField] private GameObject _fxEarnKey;
    [SerializeField] private Door _door;


    private bool _hasGetKey;

    private void GetKey()
    {
        if(_hasGetKey) return;
        
        _hasGetKey = true;
        _spriteKey.DOFade(0, 1f);
        _door.OpenDoor();
        Instantiate(_fxEarnKey, transform.position, Quaternion.identity);
    }

    public override void OnPlayerEnter()
    {
        GetKey();
    }

    protected override void ResetStartPos()
    {
        _hasGetKey = false;
        _spriteKey.DOFade(1, .5f);
        _door.CloseDoor();
    }
    
    private void OnDisable()
    {
        PlayerManager.Instance.PlayerIsDead -= ResetStartPos;
    }
}

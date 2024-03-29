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

        StartCoroutine(WaitPlayAnimPlayer());
        
        AudioManager.Instance.PlaySound("snd_collect");
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
    
    IEnumerator WaitPlayAnimPlayer()
    {
        yield return new WaitForSeconds(.25f);
        PlayerManager.Instance.PlayAnim("Bonus");
    }
}

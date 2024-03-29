using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BrokenGround : ModuleFeature
{
    [Header("--- Broken Ground ---")]
    [SerializeField] private SpriteRenderer _spriteGround;
    [SerializeField] private Sprite _brokenGround;
    [SerializeField] private GameObject _fxBrokenGround;
    [SerializeField] private GameObject _fxWillBroke;

    private Sprite _notBrokenGround;
    private Module _module;

    protected override void Start()
    {
        base.Start();
        
        _notBrokenGround = _spriteGround.sprite;
        _module = gameObject.GetComponent<Module>();
        Instantiate(_fxWillBroke, transform);
    }
    
    private void GoBroken()
    {
        _spriteGround.sprite = _brokenGround;
        _module.IsBlockedModule = true;
        
        Instantiate(_fxBrokenGround, transform.position, Quaternion.identity);
        _fxWillBroke.SetActive(false);
        
        AudioManager.Instance.PlaySound("snd_break_ground");
    }

    public override void OnPlayerLeave()
    {
        GoBroken();
    }

    protected override void ResetStartPos()
    {
        _module.IsBlockedModule = false;
        _spriteGround.sprite = _notBrokenGround;
        
        _fxWillBroke.SetActive(true);
    }
}

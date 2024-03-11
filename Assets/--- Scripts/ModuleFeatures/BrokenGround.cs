using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BrokenGround : ModuleFeature
{
    [Header("--- Broken Ground ---")]
    [SerializeField] private Image _groundImg;
    [SerializeField] private Sprite _brokenGround;

    private Sprite _notBrokenGround;
    private Module _module;

    public bool IsBrokenModule { get; set; }

    protected override void Start()
    {
        base.Start();
        
        _notBrokenGround = _groundImg.sprite;
        _module = gameObject.GetComponent<Module>();
    }
    
    private void GoBroken()
    {
        print("allo");
        _groundImg.sprite = _brokenGround;
        IsBrokenModule = true;
        _module.IsBlockedModule = true;
    }

    public override void OnPlayerLeave()
    {
        GoBroken();
    }

    protected override void ResetStartPos()
    {
        IsBrokenModule = false;
        _groundImg.sprite = _notBrokenGround;
    }
}

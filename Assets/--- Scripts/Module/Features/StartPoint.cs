using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartPoint : ModuleFeature
{
    [Header("--- StartPoint ---")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private Vector2 _imgSize;
    private Module _module;

    protected override void Start()
    {
        var imgRect = _spriteRenderer.size;
        _imgSize = new Vector2(imgRect.x, imgRect.y);
    }

    public void GoMagnetModules()
    {
        // Called by Level Parent to have only 1 module who start the magnet
        _module = gameObject.GetComponent<Module>();
        _module.Magnet(_imgSize);
    }
}

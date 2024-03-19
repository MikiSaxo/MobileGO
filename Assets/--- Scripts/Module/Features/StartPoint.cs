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
        UIManager.Instance.GoMagnetModules += GoMagnetModules;
        
        var imgRect = _spriteRenderer.size;
        _imgSize = new Vector2(imgRect.x, imgRect.y);
        print($"imgSize : {_imgSize}");

       
        // print($"----> {new Vector3(pos.x, pos.y + imgSize.y, 0)}");
    }

    public void GoMagnetModules()
    {
        _module = gameObject.GetComponent<Module>();
        _module.Magnet(_imgSize);
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        UIManager.Instance.GoMagnetModules -= GoMagnetModules;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartPoint : ModuleFeature
{
    [Header("--- StartPoint ---")]
    [SerializeField] private Image _imgSprite;
    
    private Vector2 _imgSize;
    private Module _module;

    protected override void Start()
    {
        UIManager.Instance.GoMagnetModules += GoMagnetModules;
        
        var imgRect = _imgSprite.rectTransform.rect;
        _imgSize = new Vector2(imgRect.width, imgRect.height);

       
        // print($"----> {new Vector3(pos.x, pos.y + imgSize.y, 0)}");
    }

    private void GoMagnetModules()
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

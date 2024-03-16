using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BonusCollectable : ModuleFeature
{
    [Header("--- Bonus ---")] 
    [SerializeField] private Image _bonusImg;

    private bool _hasGetBonus;

    
    
    private void GetBonus()
    {
        if (_hasGetBonus) return;
        
        _hasGetBonus = true;
        UIManager.Instance.UpdateNbBonus(1);
        _bonusImg.DOFade(0, 1f);
    }
    
    public override void OnPlayerEnter()
    {
        GetBonus();
    }

    protected override void ResetStartPos()
    {
        if(_hasGetBonus)
            UIManager.Instance.UpdateNbBonus(-1);
        _hasGetBonus = false;
        _bonusImg.DOFade(1, .5f);
    }
}

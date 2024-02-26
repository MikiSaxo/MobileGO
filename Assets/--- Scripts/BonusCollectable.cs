using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BonusCollectable : Module
{
    [Header("--- Bonus ---")] 
    [SerializeField] private Image _bonusImg;

    private bool _hasGetBonus;
    
    public void GetBonus()
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
}

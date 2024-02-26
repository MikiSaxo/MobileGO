using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BonusCollectable : Module
{
    [Header("--- Bonus ---")] 
    [SerializeField] private Image _bonusImg;
    
    public bool HasGetBonus { get; private set; }
    
    public void GetBonus()
    {
        UIManager.Instance.UpdateNbBonus(1);
        HasGetBonus = true;
        _bonusImg.DOFade(0, 1f);
    }
}

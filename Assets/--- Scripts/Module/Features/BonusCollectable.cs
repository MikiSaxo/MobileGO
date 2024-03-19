using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BonusCollectable : ModuleFeature
{
    [Header("--- Bonus ---")] 
    [SerializeField] private SpriteRenderer _spriteBonus;
    [SerializeField] private GameObject _fxEarnCrown;

    private bool _hasGetBonus;

    private void GetBonus()
    {
        if (_hasGetBonus) return;
        
        _hasGetBonus = true;
        UIManager.Instance.UpdateNbBonus(1);
        _spriteBonus.DOFade(0, 1f);
        Instantiate(_fxEarnCrown, transform.position, Quaternion.identity);

        StartCoroutine(WaitPlayAnimPlayer());
    }

    IEnumerator WaitPlayAnimPlayer()
    {
        yield return new WaitForSeconds(.25f);
        PlayerManager.Instance.PlayAnim("Bonus");
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
        _spriteBonus.DOFade(1, .5f);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    public event Action GoMagnetModules;
    public event Action GoMagnetWalls;

    [SerializeField] private TMP_Text _bonusTxt;

    private int _bonusCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _bonusTxt.text = $"{_bonusCount}";
        StartCoroutine(WaitMagnet());
    }

    IEnumerator WaitMagnet()
    {
        GoMagnetModules?.Invoke();
        yield return new WaitForSeconds(.1f);
        GoMagnetWalls?.Invoke();
    }

    public void UpdateNbBonus(int add)
    {
        _bonusCount += add;
        _bonusTxt.text = $"{_bonusCount}";
        if(add > 0)
            _bonusTxt.gameObject.transform.DOPunchScale(Vector3.one, .5f);
    }
}

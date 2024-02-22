using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public event Action PlayerHasSwipe;
    
    [SerializeField] private Module _currentModule;


    private void Awake()
    {
        Instance = this;
    }

    public void WantToSwipe(Directions dir)
    {
        var mod = _currentModule.GetModuleNeighbor(dir);
        if (mod == null) return;

        _currentModule = mod;
        gameObject.transform.DOComplete();
        gameObject.transform.DOMove(_currentModule.gameObject.transform.position, .5f);
        PlayerHasSwipe?.Invoke();
    }
}

public enum Directions
{
    Top = 0,
    Left = 1,
    Right = 2,
    Down = 3
}

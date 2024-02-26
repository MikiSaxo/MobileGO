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
    [SerializeField] private float _timeToMove = .5f;

    private Module _startModule;
    private bool _isDead;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _startModule = _currentModule;
    }

    public void WantToSwipe(Directions dir)
    {
        if (_isDead) return;

        var mod = _currentModule.GetModuleNeighbor(dir);

        if (CheckIfCanSwipe(mod) == false) return;
    
        mod.OnPlayerEnter();
        
        _currentModule = mod;
        gameObject.transform.DOComplete();
        gameObject.transform.DOMove(_currentModule.gameObject.transform.position, _timeToMove);
        PlayerHasSwipe?.Invoke();
        
        CheckIsDead();
    }

    private bool CheckIfCanSwipe(Module mod)
    {
        if (mod == null) return false;

        return mod.CanMove();
    }

    private void CheckIfEnd(Module mod)
    {
        
    }

    private void CheckIsDead()
    {
        if(_currentModule.IsDeathModule)
            StartCoroutine(GoDeath());
    }

    IEnumerator GoDeath()
    {
        _isDead = true;
        
        yield return new WaitForSeconds(_timeToMove);
        
        gameObject.transform.DOMove(_startModule.gameObject.transform.position, 0f);
        _currentModule = _startModule;
        _isDead = false;
    }
}

public enum Directions
{
    Top = 0,
    Left = 1,
    Right = 2,
    Down = 3
}

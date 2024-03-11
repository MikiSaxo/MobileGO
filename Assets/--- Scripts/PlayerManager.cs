using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public event Action PlayerHasSwipe;
    public event Action PlayerIsDead;
    
    [SerializeField] private Module _currentModule;
    [SerializeField] private float _timeToMove = .5f;

    private Module _startModule;
    private bool _isDead;
    private bool _isEnd;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _startModule = _currentModule;
        gameObject.transform.DOMove(_currentModule.gameObject.transform.position, 0);
    }

    public void WantToSwipe(Directions dir)
    {
        if (_isDead) return;

        _currentModule.OnPlayerLeave();

        var mod = _currentModule.GetModuleNeighbor(dir);

        if (CheckIfCanSwipe(mod) == false) return;
        
        _currentModule = mod;
        gameObject.transform.DOComplete();
        gameObject.transform.DOMove(_currentModule.gameObject.transform.position, _timeToMove);
        
        PlayerHasSwipe?.Invoke();
        mod.OnPlayerEnter();
        
        if (_isEnd) StartCoroutine(WaitToGoNextStation(_startModule));
        
        CheckIsDead();
    }

    private bool CheckIfCanSwipe(Module mod)
    {
        if (mod == null) return false;

        return mod.CanMove();
    }

    public void GoStartPoint(Module mod)
    {
        _isEnd = true;
        _startModule = mod;
    }

    IEnumerator WaitToGoNextStation(Module mod)
    {
        yield return new WaitForSeconds(_timeToMove);
        
        _currentModule = mod;
        gameObject.transform.DOKill();
        gameObject.transform.DOMove(_currentModule.gameObject.transform.position, _timeToMove);
        
        yield return new WaitForSeconds(_timeToMove);
        
        _isEnd = false;
    }

    public void CheckIsDead()
    {
        if(_currentModule.IsDeathModule)
            StartCoroutine(GoDeath());
    }

    public void KillPlayer()
    {
        StartCoroutine(GoDeath());
    }

    IEnumerator GoDeath()
    {
        _isDead = true;
        
        // yield return new WaitForSeconds(_timeToMove*.5f);
        

        yield return new WaitForSeconds(_timeToMove);
        
        PlayerIsDead?.Invoke();
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

using System;
using System.Collections;
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
    public bool CanMove { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _startModule = _currentModule;
        CanMove = true;
    }

    public void GoStartPos()
    {
        gameObject.transform.position = _currentModule.gameObject.transform.position;
    }

    public void WantToSwipe(Directions dir)
    {
        if (_isDead || CanMove == false) return;

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

    public void GoNextStartPoint(Module mod)
    {
        _isEnd = true;
        _startModule = mod;
        
        StartCoroutine(WaitToGoNextStation(_startModule));
    }

    IEnumerator WaitToGoNextStation(Module mod)
    {
        CanMove = false;

        yield return new WaitForSeconds(_timeToMove);
        
        _currentModule = mod;
        gameObject.transform.DOKill();
        gameObject.transform.DOMove(_currentModule.gameObject.transform.position, _timeToMove);
        
        yield return new WaitForSeconds(_timeToMove);
        
        _isEnd = false;
        CanMove = true;
    }

    public void CheckIsDead()
    {
        if(_currentModule.IsDeathModule)
            StartCoroutine(WaitGoDeath());
    }

    public void KillPlayer()
    {
        StartCoroutine(WaitGoDeath());
    }

    IEnumerator WaitGoDeath()
    {
        _isDead = true;
        
        yield return new WaitForSeconds(_timeToMove);
        
        GoDeath();
    }

    public void GoDeath()
    {
        _isDead = true;

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

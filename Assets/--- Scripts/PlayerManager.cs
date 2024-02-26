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
        
        if (mod == null) return;
        
        if (mod.GetComponent<WeakGround>() != null && mod.GetComponent<WeakGround>().IsWeakModule) return;
            
        if (mod.GetComponent<Door>() != null && !mod.GetComponent<Door>().IsOpen) return;

        if (mod.GetComponent<Key>() != null && mod.GetComponent<Key>().HasGetKey == false)
            mod.GetComponent<Key>().GetKey();

        if (_currentModule.GetComponent<WeakGround>() != null)
            _currentModule.GetComponent<WeakGround>().GoBroken();
        
        _currentModule = mod;
        gameObject.transform.DOComplete();
        gameObject.transform.DOMove(_currentModule.gameObject.transform.position, _timeToMove);
        PlayerHasSwipe?.Invoke();
        CheckIsDead();
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

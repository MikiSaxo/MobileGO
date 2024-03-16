using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    [SerializeField] private Transform[] _levelPoints;
    [SerializeField] private GameObject[] _allLevels;
    [SerializeField] private float _timeToExitLevel;
    [SerializeField] private float _timeToEnterLevel;

    private int _currentLevel;
    private Module _nextStartPoint;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetPosAllLevels();
    }

    private void SetPosAllLevels()
    {
        for (int i = 1; i < _allLevels.Length; i++)
        {
            _allLevels[i].transform.position = _levelPoints[2].position;
        }
        _allLevels[0].transform.position = _levelPoints[1].position;
        
        PlayerManager.Instance.GoStartPos();
    }

    public void ChangeLevel(Module nextStartPoint)
    {
        _nextStartPoint = nextStartPoint;
        
        
        _currentLevel++;

        if (_currentLevel == _allLevels.Length)
        {
            UIManager.Instance.EndGame();
            return;
        }

        PlayerManager.Instance.CanMove = false;
        
        _allLevels[_currentLevel - 1].transform.DOMove(_levelPoints[0].position, _timeToExitLevel).SetEase(Ease.InExpo);

        if(_nextStartPoint != null)
            _allLevels[_currentLevel].transform.DOMove(_levelPoints[1].position, _timeToEnterLevel).SetEase(Ease.InExpo).OnComplete(TpPlayer);
        else
        {
            _allLevels[_currentLevel].transform.DOMove(_levelPoints[1].position, _timeToEnterLevel).SetEase(Ease.InExpo);
            Debug.LogWarning("Next Start Point  not set - Can't TP Player properly");
        }
    }

    private void TpPlayer()
    {
        PlayerManager.Instance.GoNextStartPoint(_nextStartPoint);
    }

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        //     ChangeLevel(null);
    }
}
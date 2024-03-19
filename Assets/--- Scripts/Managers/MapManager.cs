using System;
using System.Collections;
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

        SpawnNewLevel();
      
        
        // _allLevels[_currentLevel].transform.DOMove(_levelPoints[1].position, _timeToEnterLevel).SetEase(Ease.InExpo).OnComplete(TpPlayer);
        StartCoroutine(WaitMoveLevel());


        // PlayerManager.Instance.GoStartPos();
    }

    private void SpawnNewLevel()
    {
        GameObject newLvl = Instantiate(_allLevels[_currentLevel], _levelPoints[0].position, Quaternion.identity);
        _nextStartPoint = newLvl.GetComponent<Level>().StartModule;
        _allLevels[_currentLevel] = newLvl;
    }

    public void ChangeLevel()
    {
        _currentLevel++;

        if (_currentLevel == _allLevels.Length)
        {
            UIManager.Instance.EndGame();
            return;
        }

        SpawnNewLevel();

        PlayerManager.Instance.CanMove = false;


        StartCoroutine(WaitMoveLevel());
    }

    IEnumerator WaitMoveLevel()
    {
        yield return new WaitForSeconds(.2f);
        
        if(_currentLevel != 0)
            _allLevels[_currentLevel - 1].transform.DOMove(_levelPoints[2].position, _timeToExitLevel).SetEase(Ease.InExpo);

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
        _allLevels[_currentLevel-1].SetActive(false);
    }

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        //     ChangeLevel(null);
    }
}

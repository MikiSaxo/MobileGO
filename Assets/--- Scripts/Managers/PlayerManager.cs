using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public event Action PlayerHasSwipe;
    public event Action PlayerHasSwipe2;
    public event Action PlayerIsDead;
    
    [SerializeField] private float _timeToMove = .5f;

    private Module _currentModule;
    private Module _startModule;
    private bool _isDead;
    private bool _isEnd;
    private bool _isRestarting;
    private Directions _saveLastDir;
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

    public void WantToSwipe(Directions dir)
    {
        if (_isDead || CanMove == false) return;

        _currentModule.OnPlayerLeave();

        var mod = _currentModule.GetModuleNeighbor(dir);

        if (CheckIfCanSwipe(mod) == false) return;

        _saveLastDir = dir;
        _currentModule = mod;
        gameObject.transform.DOComplete();
        gameObject.transform.DOMove(_currentModule.gameObject.transform.position, _timeToMove).SetEase(Ease.OutSine);
        
        PlayerHasSwipe?.Invoke();
        PlayerHasSwipe2?.Invoke();
        mod.OnPlayerEnter();
        
        if (_isEnd) StartCoroutine(WaitToGoNextStation(_startModule));
        
        CheckIsDead();
        
        AudioManager.Instance.PlaySound("snd_walk");
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
        
        UIManager.Instance.UpdateRestartBtn(true);
    }

    public bool CheckIsDead()
    {
        if (_currentModule.IsDeathModule)
        {
            StartCoroutine(WaitGoDeath(false));
        }

        return _currentModule.IsDeathModule;
    }

    public void KillPlayerRestart()
    {
        if (_isRestarting) return;
        
        StartCoroutine(WaitGoDeath(true));
        _isRestarting = true;
        AudioManager.Instance.PlaySound("snd_death_restart");
    }
    public void KillPlayer()
    {
        StartCoroutine(WaitGoDeath(false));
    }

    IEnumerator WaitGoDeath(bool isRestart)
    {
        _isDead = true;

        if(!isRestart)
            yield return new WaitForSeconds(_timeToMove-.2f);

        if (!isRestart)
        {
            PlayAnim("Death");
        }
        else
        {
            gameObject.GetComponent<SpriteView>().PlayAction($"DeathDown");
        }
        
        yield return new WaitForSeconds(_timeToMove);
        
        GoDeath();
    }

    public void GoDeath()
    {
        _isDead = true;

        PlayerIsDead?.Invoke();
        gameObject.transform.position = _startModule.gameObject.transform.position;
        _currentModule = _startModule;
        UIManager.Instance.GetHit();
        _isRestarting = false;
        
        _isDead = false;
    }
    
    public void PlayAnim(string keyWord)
    {
        if(_saveLastDir == Directions.Down)
            gameObject.GetComponent<SpriteView>().PlayAction($"{keyWord}Down");
        else if(_saveLastDir == Directions.Top)
            gameObject.GetComponent<SpriteView>().PlayAction($"{keyWord}Up");
        else if(_saveLastDir == Directions.Left)
            gameObject.GetComponent<SpriteView>().PlayAction($"{keyWord}Left");
        else if(_saveLastDir == Directions.Right)
            gameObject.GetComponent<SpriteView>().PlayAction($"{keyWord}Right");
    }
}

public enum Directions
{
    Top = 0,
    Left = 1,
    Right = 2,
    Down = 3
}

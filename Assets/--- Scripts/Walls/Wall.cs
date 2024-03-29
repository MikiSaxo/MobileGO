using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    [SerializeField] private bool _startNextState;
    [Header("--- Stay In Place ---")]
    [SerializeField] private SpriteRenderer _spriteWallInPlace;
    [SerializeField] private Sprite[] _wallInPlaceSprites;
    [Header("--- Walls Infos --- ")]
    [SerializeField] protected WallInfos[] _wallInfos;

    private int _count = 0;
    private int _startCount = 0;

    protected bool _isBlocked = false;
    private bool _hasInit = false;


    private void Start()
    {
        PlayerManager.Instance.PlayerHasSwipe += ChangePos;
        PlayerManager.Instance.PlayerIsDead += ResetStartPos;
    }

    private void ChangePos()
    {
        if (_wallInfos.Length <= 1) return;
        if (_isBlocked) return;

        ResetOldWall();

        _count++;

        if (_count >= _wallInfos.Length)
            _count = 0;

        Move(); 

        SetWallToModule();
    }

    protected void Move()
    {
        gameObject.transform.DOComplete();
        if (_wallInfos[_count].NewPos != null)
        {
            gameObject.transform.DOMove(_wallInfos[_count].NewPos.position, .5f);
            gameObject.transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutBounce);

            if (_spriteWallInPlace != null && _wallInPlaceSprites.Length > 0)
            {
                _spriteWallInPlace.sprite = _wallInPlaceSprites[0];
            }
        }
        else
        {
            if(_spriteWallInPlace != null && _wallInPlaceSprites.Length > 0)
                _spriteWallInPlace.sprite = _wallInPlaceSprites[1];
        }
    }

    protected void SetWallToModule()
    {
        if (_isBlocked) return;

        var topMod = _wallInfos[_count].ModuleAtTop;
        if (topMod != null)
            topMod.AddWall(new []{ null, null, null, this});

        var leftMod = _wallInfos[_count].ModuleAtLeft;
        if (leftMod != null)
            leftMod.AddWall(new []{null, null, this, null});

        var rightMod = _wallInfos[_count].ModuleAtRight;
        if (rightMod != null)
            rightMod.AddWall(new []{null, this, null, null});

        var downMod = _wallInfos[_count].ModuleAtDown;
        if (downMod != null)
            downMod.AddWall(new []{this, null, null, null});
    }

    public void GoWaitToReplace()
    {
        if (_startNextState)
        {
            ChangePos();
            _startCount = 0;
        }
        else
        {
            SetWallToModule();
            Move();
            _startCount = _wallInfos.Length-1;
        }
        
        if (!_hasInit)
        {
            gameObject.transform.DOComplete();
            gameObject.transform.DOMove(_wallInfos[0].NewPos.position, 0);
            gameObject.transform.DOComplete();
            _hasInit = true;
        }
    }

    protected void ResetOldWall()
    {
        var topMod = _wallInfos[_count].ModuleAtTop;
        if (topMod != null)
            topMod.ResetWall();

        var leftMod = _wallInfos[_count].ModuleAtLeft;
        if (leftMod != null)
            leftMod.ResetWall();

        var rightMod = _wallInfos[_count].ModuleAtRight;
        if (rightMod != null)
            rightMod.ResetWall();

        var downMod = _wallInfos[_count].ModuleAtDown;
        if (downMod != null)
            downMod.ResetWall();
    }

    private void ResetStartPos()
    {
        _count = _startCount;
        ChangePos();
    }

    private void OnDrawGizmosSelected()
    {
        if (_wallInfos.Length == 0) return;
        
        foreach (var wall in _wallInfos)
        {
            if (wall.NewPos == null) return;

            var pos = wall.NewPos.position;
            Gizmos.color = Color.red;
            if (wall.ModuleAtTop != null)
                Gizmos.DrawLine(pos, wall.ModuleAtTop.gameObject.transform.position);

            Gizmos.color = Color.yellow;
            if (wall.ModuleAtLeft != null)
                Gizmos.DrawLine(pos, wall.ModuleAtLeft.gameObject.transform.position);

            Gizmos.color = Color.green;
            if (wall.ModuleAtDown != null)
                Gizmos.DrawLine(pos, wall.ModuleAtDown.gameObject.transform.position);

            Gizmos.color = Color.blue;
            if (wall.ModuleAtRight != null)
                Gizmos.DrawLine(pos, wall.ModuleAtRight.gameObject.transform.position);
        }
    }

    private void OnDisable()
    {
        PlayerManager.Instance.PlayerHasSwipe -= ChangePos;
        PlayerManager.Instance.PlayerIsDead -= ResetStartPos;
    }
}
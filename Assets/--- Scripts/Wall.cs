using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private WallInfos[] _wallInfos;

    private int _count = 0;

    private void Start()
    {
        PlayerManager.Instance.PlayerHasSwipe += ChangePos;
    }

    private void ChangePos()
    {
        if (_wallInfos.Length <= 1) return;
        
        ResetOldWall();
        
        _count++;
        
        if (_count >= _wallInfos.Length)
            _count = 0;

        gameObject.transform.DOComplete();
        gameObject.transform.DOMove(_wallInfos[_count].NewPos.position, .5f);
        
        
        SetWallToModule();
    }

    private void SetWallToModule()
    {
        var topMod = _wallInfos[_count].ModuleAtTop;
        if (topMod != null)
            topMod.AddWall(null, null,null,this);
        
        var leftMod = _wallInfos[_count].ModuleAtLeft;
        if (leftMod != null)
            leftMod.AddWall(null, null,this,null);
        
        var rightMod = _wallInfos[_count].ModuleAtRight;
        if (rightMod != null)
            rightMod.AddWall(null, this,null,null);
        
        var downMod = _wallInfos[_count].ModuleAtDown;
        if (downMod != null)
            downMod.AddWall(this, null,null,null);
    }

    private void ResetOldWall()
    {
        var topMod = _wallInfos[_count].ModuleAtTop;
        if (topMod != null)
            topMod.AddWall(null, null,null,null);
        
        var leftMod = _wallInfos[_count].ModuleAtLeft;
        if (leftMod != null)
            leftMod.AddWall(null, null,null,null);
        
        var rightMod = _wallInfos[_count].ModuleAtRight;
        if (rightMod != null)
            rightMod.AddWall(null, null,null,null);
        
        var downMod = _wallInfos[_count].ModuleAtDown;
        if (downMod != null)
            downMod.AddWall(null, null,null,null);
    }

    private void OnDrawGizmosSelected()
    {
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
    }
}
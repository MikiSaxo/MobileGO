using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleFeature : MonoBehaviour
{
    protected virtual void Start()
    {
        PlayerManager.Instance.PlayerHasSwipe += ChangeState;
        PlayerManager.Instance.PlayerIsDead += ResetStartPos;
    }

    protected virtual void ChangeState()
    {
    }
    protected virtual void ResetStartPos()
    {
    }

    public virtual void OnPlayerEnter()
    {
    }
    public virtual void OnPlayerLeave()
    {
    }
    
    private void OnDisable()
    {
        PlayerManager.Instance.PlayerHasSwipe -= ChangeState;
        PlayerManager.Instance.PlayerIsDead -= ResetStartPos;
    }
}

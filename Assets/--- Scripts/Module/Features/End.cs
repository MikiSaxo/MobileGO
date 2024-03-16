using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : ModuleFeature
{
    [Header("--- End ---")] 
    [SerializeField] private Module _nextStartPoint;
    
    public override void OnPlayerEnter()
    {
        MapManager.Instance.ChangeLevel(_nextStartPoint);
    }
}

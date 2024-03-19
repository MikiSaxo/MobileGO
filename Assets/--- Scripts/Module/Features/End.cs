using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : ModuleFeature
{
    // [Header("--- End ---")] 
    // [SerializeField] private Module _nextStartPoint;
    
    public override void OnPlayerEnter()
    {
        MapManager.Instance.ChangeLevel();
        UIManager.Instance.UpdateRestartBtn(false);
        AudioManager.Instance.PlaySound("snd_end_level");
    }
}

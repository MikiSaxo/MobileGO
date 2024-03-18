using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct State
{
    public string Name;
    
    public float TimeBetweenFrames;
    
    public List<Sprite> SpriteSheet;
}


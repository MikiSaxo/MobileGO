using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class WallInfos
{
    public Module ModuleAtTop;
    public Module ModuleAtLeft;
    public Module ModuleAtRight;
    public Module ModuleAtDown;
    public Transform NewPos;
}

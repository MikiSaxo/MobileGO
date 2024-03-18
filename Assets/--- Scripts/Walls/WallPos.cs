using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class WallPos : MonoBehaviour
{
    public Module ModuleAtTop;
    public Module ModuleAtLeft;
    public Module ModuleAtRight;
    public Module ModuleAtDown;

    private const float HalfSizeSprite = .15f;
    private void Start()
    {
        UIManager.Instance.GoMagnetWalls += GoMagnet;
    }

    private void GoMagnet()
    {
        if (ModuleAtTop != null)
        {
            var localPosition = ModuleAtTop.gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y - HalfSizeSprite, 0);
        }
        
        if (ModuleAtLeft != null)
        {
            var localPosition = ModuleAtLeft.gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(localPosition.x + HalfSizeSprite, localPosition.y, 0);
        }
        
        if (ModuleAtRight != null)
        {
            var localPosition = ModuleAtRight.gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(localPosition.x - HalfSizeSprite, localPosition.y, 0);
        }
        
        if (ModuleAtDown != null)
        {
            var localPosition = ModuleAtDown.gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y + HalfSizeSprite, 0);
        }
    }
    
    private void OnDisable()
    {
        UIManager.Instance.GoMagnetWalls -= GoMagnet;
    }
}

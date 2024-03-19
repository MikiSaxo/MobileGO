using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPos : MonoBehaviour
{
    public Module ModuleAtTop;
    public Module ModuleAtLeft;
    public Module ModuleAtRight;
    public Module ModuleAtDown;

    private const float HalfSizeSprite = 0.16f;

    public void GoMagnet()
    {
        if (ModuleAtTop != null)
        {
            var localPosition = ModuleAtTop.gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y - HalfSizeSprite, 0);
            return;
        }

        if (ModuleAtLeft != null)
        {
            var localPosition = ModuleAtLeft.gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(localPosition.x + HalfSizeSprite, localPosition.y, 0);
            return;
        }

        if (ModuleAtRight != null)
        {
            var localPosition = ModuleAtRight.gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(localPosition.x - HalfSizeSprite, localPosition.y, 0);
            return;
        }

        if (ModuleAtDown != null)
        {
            var localPosition = ModuleAtDown.gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(localPosition.x, localPosition.y + HalfSizeSprite, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        var pos = gameObject.transform.position;
        Gizmos.color = Color.red;
        if (ModuleAtTop != null)
            Gizmos.DrawLine(pos, ModuleAtTop.gameObject.transform.position);

        Gizmos.color = Color.yellow;
        if (ModuleAtLeft != null)
            Gizmos.DrawLine(pos, ModuleAtLeft.gameObject.transform.position);

        Gizmos.color = Color.green;
        if (ModuleAtDown != null)
            Gizmos.DrawLine(pos, ModuleAtDown.gameObject.transform.position);

        Gizmos.color = Color.blue;
        if (ModuleAtRight != null)
            Gizmos.DrawLine(pos, ModuleAtRight.gameObject.transform.position);
    }
}
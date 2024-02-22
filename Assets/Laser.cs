using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    [SerializeField] private Module[] _modulesToLaser;
    [SerializeField] private Image _laserImg;
    [SerializeField] private float[] _differentHeightLaser;

    private int _count = 0;
    
    private void Start()
    {
        PlayerManager.Instance.PlayerHasSwipe += ChangeState;
    }

    private void ChangeState()
    {
        _count++;

        if (_count >= _differentHeightLaser.Length)
            _count = 0;

        Vector2 size = _laserImg.rectTransform.sizeDelta;
        size.y = _differentHeightLaser[_count];
        _laserImg.rectTransform.sizeDelta = size;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        foreach (var mod in _modulesToLaser)
        {
            if (mod != null)
            {
                Gizmos.DrawSphere(mod.gameObject.transform.position, .05f);
            }
        }
    }
    
    private void OnDisable()
    {
        PlayerManager.Instance.PlayerHasSwipe -= ChangeState;
    }
}

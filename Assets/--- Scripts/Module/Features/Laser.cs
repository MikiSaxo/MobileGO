using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    [SerializeField] private Module[] _modulesToLaser;
    
    [SerializeField] private SpriteRenderer _laserModuleImg;
    [SerializeField] private SpriteRenderer _laserRayImg;
    
    [SerializeField] private Sprite[] _spritesLaser;
    [SerializeField] private Sprite[] _spritesLaserModule;

    private List<Image> _laserPosPrefabs = new List<Image>();

    private int _count = 0;
    
    private void Start()
    {
        PlayerManager.Instance.PlayerHasSwipe += ChangeState;
        PlayerManager.Instance.PlayerIsDead += ResetStartPos;
    }

    private void ResetStartPos()
    {
        _count = _spritesLaser.Length;
        ChangeState();
    }

    private void ChangeState()
    {
        _count++;

        if (_count >= _spritesLaser.Length)
        {
            _count = 0;
            foreach (var mod in _modulesToLaser)
            {
                mod.IsDeathModule = false;
            }
        }

        _laserRayImg.sprite = _spritesLaser[_count];
        _laserRayImg.transform.DOScale(_spritesLaser[_count] == null ? 0 : 1, 0);
        _laserModuleImg.sprite = _spritesLaserModule[_count];

        if (_count == _spritesLaser.Length - 1)
        {
            foreach (var mod in _modulesToLaser)
            {
                mod.IsDeathModule = true;
            }
        }
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
        PlayerManager.Instance.PlayerIsDead += ResetStartPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    [Header("--- My Module")]
    [SerializeField] private SpriteRenderer _laserModuleImg;
    [SerializeField] private Sprite[] _spritesLaserModule;
    
    [Header("--- On Module")]
    [SerializeField] private GameObject _laserOnModulePrefab;
    [SerializeField] private Sprite[] _spritesLaserOnModule;
    [SerializeField] private Module[] _modulesToLaser;
    

    private List<LaserOnModule> _lasersOnModule = new List<LaserOnModule>();

    private int _count = 0;
    
    private void Start()
    {
        PlayerManager.Instance.PlayerHasSwipe += ChangeState;
        PlayerManager.Instance.PlayerIsDead += ResetStartPos;

        for (int i = 0; i < _modulesToLaser.Length; i++)
        {
            GameObject go = Instantiate(_laserOnModulePrefab, _modulesToLaser[i].transform);
            _lasersOnModule.Add(go.GetComponent<LaserOnModule>());
            go.GetComponent<LaserOnModule>().ChangeSprite(_spritesLaserOnModule[0]);
        }
    }

    private void ResetStartPos()
    {
        _count = _spritesLaserOnModule.Length;
        ChangeState();
    }

    private void ChangeState()
    {
        _count++;

        if (_count >= _spritesLaserOnModule.Length)
        {
            _count = 0;
            foreach (var mod in _modulesToLaser)
            {
                mod.IsDeathModule = false;
            }
        }

        foreach (var laser in _lasersOnModule)
        {
            laser.ChangeSprite(_spritesLaserOnModule[_count]);
        }

        // _laserRayImg.sprite = _spritesLaser[_count];
        // _laserRayImg.transform.DOScale(_spritesLaser[_count] == null ? 0 : 1, 0);
        _laserModuleImg.sprite = _spritesLaserModule[_count];

        if (_count == _spritesLaserOnModule.Length - 1)
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

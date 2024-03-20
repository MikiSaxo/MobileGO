using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    [Header("--- My Module")] [SerializeField]
    private SpriteRenderer _laserModuleImg;

    [SerializeField] private Sprite[] _spritesLaserModule;
    [SerializeField] private Directions _myDirection;

    [Header("--- On Module")] [SerializeField]
    private int _laserStateNumber = 4;

    [SerializeField] private GameObject _laserOnModulePrefab;
    [SerializeField] private Module[] _modulesToLaser;


    private List<LaserOnModule> _lasersOnModule = new List<LaserOnModule>();

    private int _count = 0;
    private bool _isBlock;
    private bool _isDead;

    private void Start()
    {
        PlayerManager.Instance.PlayerHasSwipe += ResetDeathModules;
        PlayerManager.Instance.PlayerHasSwipe2 += GoChangeState;
        PlayerManager.Instance.PlayerIsDead += ResetStartPos;

        for (int i = 0; i < _modulesToLaser.Length; i++)
        {
            GameObject go = Instantiate(_laserOnModulePrefab, _modulesToLaser[i].transform);
            _lasersOnModule.Add(go.GetComponent<LaserOnModule>());

            go.GetComponent<LaserOnModule>().Init(_myDirection);
            go.GetComponent<LaserOnModule>().ChangeSprite(0);
        }
    }

    private void GoChangeState()
    {
        if (!_isDead)
        {
            ChangeState();
        }
    }

    private void ResetStartPos()
    {
        _count = _laserStateNumber;
        ChangeState();
    }

    private void ChangeState()
    {
        _count++;
        _isBlock = false;


        if (_count >= _laserStateNumber)
        {
            _count = 0;
        }

        for (int i = 0; i < _modulesToLaser.Length; i++)
        {
            var laser = _lasersOnModule[i];
            var mod = _modulesToLaser[i];

            // Block sprite or not
            if (!_isBlock && _count <= _laserStateNumber - 1)
            {
                laser.ChangeSprite(_count);
                // Death module if not block
                if (_count == _laserStateNumber - 1)
                    mod.IsDeathModule = true;
            }
            else
            {
                laser.ChangeSprite(0);
                mod.IsDeathModule = false;
            }


            if (mod.GetWallSide((int)_myDirection) != null)
            {
                _isBlock = true;
            }
        }

        if (PlayerManager.Instance.CheckIsDead())
        {
            AudioManager.Instance.PlaySound("snd_death_fire");
            ResetDeathModules();
        }

        if (_count <= _laserStateNumber - 1)
            _laserModuleImg.sprite = _spritesLaserModule[_count];

        if (_count == _laserStateNumber - 1)
            AudioManager.Instance.PlaySound("snd_dragon_fire");
    }

    private void ResetDeathModules()
    {
        foreach (var mod in _modulesToLaser)
        {
            mod.IsDeathModule = false;
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
        _isDead = true;
        PlayerManager.Instance.PlayerHasSwipe2 -= ChangeState;
        PlayerManager.Instance.PlayerIsDead -= ResetStartPos;
    }
}
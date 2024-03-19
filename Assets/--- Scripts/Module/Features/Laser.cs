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
    private GameObject _laserOnModulePrefab;

    [SerializeField] private Sprite[] _spritesLaserOnModule;
    [SerializeField] private Module[] _modulesToLaser;


    private List<LaserOnModule> _lasersOnModule = new List<LaserOnModule>();

    private int _count = 0;
    private bool _isBlock;

    private void Start()
    {
        PlayerManager.Instance.PlayerHasSwipe += GoChangeState;
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
        StartCoroutine(WaitChangeState());
    }

    private void ResetStartPos()
    {
        _count = _spritesLaserOnModule.Length;
        ChangeState();
    }

    IEnumerator WaitChangeState()
    {
        // Reset all DeathModule
        ResetDeathModules();

        yield return new WaitForSeconds(.1f);

        ChangeState();
    }

    private void ChangeState()
    {
        _count++;
        _isBlock = false;


        for (int i = 0; i < _modulesToLaser.Length; i++)
        {
            var laser = _lasersOnModule[i];
            var mod = _modulesToLaser[i];

            // Block sprite or not
            if (!_isBlock && _count <= _spritesLaserOnModule.Length - 1)
                laser.ChangeSprite(_count);
            else
                laser.ChangeSprite(0);


            // Death module if not block
            if (_count == _spritesLaserOnModule.Length - 1 && !_isBlock)
                mod.IsDeathModule = true;


            if (mod.GetWallSide((int)_myDirection) != null)
            {
                _isBlock = true;
            }
        }

        if (PlayerManager.Instance.CheckIsDead())
        {
            ResetDeathModules();
        }

        if (_count <= _spritesLaserOnModule.Length - 1)
            _laserModuleImg.sprite = _spritesLaserModule[_count];
    }

    private void ResetDeathModules()
    {
        if (_count >= _spritesLaserOnModule.Length - 1)
        {
            _count = 0;
            foreach (var mod in _modulesToLaser)
            {
                mod.IsDeathModule = false;
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
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Door : Wall
{
    [Header("--- Door ---")] 
    [SerializeField] private SpriteRenderer _spriteDoor;
    [SerializeField] private GameObject _fxOpenDoor;

    private Vector3 _startSize;


    private void Start()
    {
        PlayerManager.Instance.PlayerHasSwipe += SetWallToModule;

        _startSize = _spriteDoor.gameObject.transform.localScale;
        
        SetWallToModule();
    }

    public void OpenDoor()
    {
        _isBlocked = true;
        
        ResetOldWall();

        _spriteDoor.gameObject.transform.DOComplete();
        _spriteDoor.gameObject.transform.DOScale(Vector3.zero, .5f);
        Instantiate(_fxOpenDoor, transform.position, Quaternion.identity);
        
        AudioManager.Instance.PlaySound("snd_door_open");
    }

    public void CloseDoor()
    {
        _isBlocked = false;

        SetWallToModule();

        _spriteDoor.gameObject.transform.DOComplete();
        _spriteDoor.gameObject.transform.DOScale(_startSize, .5f);
    }
    
    private void OnDisable()
    {
        PlayerManager.Instance.PlayerHasSwipe -= SetWallToModule;
    }
}
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
        UIManager.Instance.GoMagnetWalls += Move;

        _startSize = _spriteDoor.gameObject.transform.localScale;
        
        SetWallToModule();

        //gameObject.transform.DOMove(_wallInfos[0].NewPos.gameObject.transform.localPosition, 0f);
        //gameObject.transform.position = _wallInfos[0].NewPos.localPosition;

        
        //StartCoroutine(WaitToTakePosition());
    }

    IEnumerator WaitToTakePosition()
    {
        yield return new WaitForSeconds(2f);
        print($"take pos local : {_wallInfos[0].NewPos.localPosition}");
        // print($"take pos : {_wallInfos[0].NewPos.gameObject.transform.position}");
        Move();
        //gameObject.transform.position = _wallInfos[0].NewPos.localPosition;
    }

    public void OpenDoor()
    {
        _isBlocked = true;
        
        ResetOldWall();

        _spriteDoor.gameObject.transform.DOComplete();
        _spriteDoor.gameObject.transform.DOScale(Vector3.zero, .5f);
        Instantiate(_fxOpenDoor, transform.position, Quaternion.identity);
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
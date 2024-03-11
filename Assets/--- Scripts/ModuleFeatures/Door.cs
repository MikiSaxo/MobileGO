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
    [SerializeField] private Image _doorImg;



    private void Start()
    {
        PlayerManager.Instance.PlayerHasSwipe += SetWallToModule;

        SetWallToModule();
    }

    public void OpenDoor()
    {
        PlayerManager.Instance.PlayerHasSwipe -= SetWallToModule;

        ResetOldWall();

        _doorImg.gameObject.transform.DOComplete();
        _doorImg.gameObject.transform.DOScale(Vector3.one * .5f, .5f);
    }

    public void CloseDoor()
    {
        PlayerManager.Instance.PlayerHasSwipe += SetWallToModule;
        
        SetWallToModule();

        _doorImg.gameObject.transform.DOComplete();
        _doorImg.gameObject.transform.DOScale(Vector3.one, .5f);
    }
    
    private void OnDisable()
    {
        PlayerManager.Instance.PlayerHasSwipe -= SetWallToModule;
    }
}
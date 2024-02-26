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
        SetWallToModule();
    }

    public void OpenDoor()
    {
        ResetOldWall();

        _doorImg.gameObject.transform.DOComplete();
        _doorImg.gameObject.transform.DOScale(Vector3.one * .5f, .5f);
    }
}
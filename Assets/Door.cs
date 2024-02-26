using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Door : Module
{
    [Header("--- Door ---")] 
    [SerializeField] private Image _doorImg;

    public bool IsOpen { get; set; }

    private void Start()
    {
        // PlayerManager.Instance.PlayerHasSwipe += ChangeState;
    }

    public void OpenDoor()
    {
        IsOpen = true;
        
        _doorImg.gameObject.transform.DOComplete();
        _doorImg.gameObject.transform.DOScale(Vector3.one * .5f, .5f);
    }

    private void OnDisable()
    {
        // PlayerManager.Instance.PlayerHasSwipe -= ChangeState;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private Image _fade;
    [SerializeField] private float _timeFadeIn = 1.5f;
    [SerializeField] private float _timeFadeOut = .5f;

    private bool _hasLaunch;

    private void Start()
    {
        _fade.DOFade(0, _timeFadeOut);
    }

    public void GoMainScene()
    {
        if (_hasLaunch) return;

        _hasLaunch = true;
        _fade.DOFade(1, _timeFadeIn).OnComplete(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
}

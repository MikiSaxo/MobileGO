using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private Image _fade;
    [SerializeField] private float _timeFade = 1.5f;

    private bool _hasLaunch;
    
    public void GoMainScene()
    {
        if (_hasLaunch) return;

        _hasLaunch = true;
        _fade.DOFade(1, _timeFade).OnComplete(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    public event Action GoMagnetModules;
    public event Action GoMagnetWalls;

    [SerializeField] private TMP_Text _bonusTxt;
    [Header("--- Fade ---")]
    [SerializeField] private Image _fade;
    [SerializeField] private float _timeFadeOut = .5f;
    [SerializeField] private float _timeFadeIn = 1.5f;
    [Header("--- End ---")]
    [SerializeField] private TMP_Text _winText;
    [SerializeField] private float _timeSpawnWinText = 1;
    [SerializeField] private float _timeDespawnWinText = .5f;
    [SerializeField] private float _timeGoMainScene = 5;

    private int _bonusCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _bonusTxt.text = $"{_bonusCount}";
        StartCoroutine(WaitMagnet());
        _fade.DOFade(0, _timeFadeOut);
    }

    IEnumerator WaitMagnet()
    {
        GoMagnetModules?.Invoke();
        yield return new WaitForSeconds(.1f);
        GoMagnetWalls?.Invoke();
    }

    public void UpdateNbBonus(int add)
    {
        _bonusCount += add;
        _bonusTxt.text = $"{_bonusCount}";
        if(add > 0)
            _bonusTxt.gameObject.transform.DOPunchScale(Vector3.one, .5f);
    }

    public void EndGame()
    {
        StartCoroutine(AnimEndGame());
    }

    IEnumerator AnimEndGame()
    {
        _fade.DOFade(1, _timeFadeIn);

        yield return new WaitForSeconds(_timeFadeIn);

        _winText.DOFade(1, _timeSpawnWinText);
        
        yield return new WaitForSeconds(_timeGoMainScene-_timeDespawnWinText);
        
        _winText.DOFade(0, _timeDespawnWinText);
        
        yield return new WaitForSeconds(_timeDespawnWinText);
        
        SceneManager.LoadScene(0);
    }
}

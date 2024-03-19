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
    
    [Header("--- Bonus ---")]
    [SerializeField] private TMP_Text _bonusTxt;
    [SerializeField] private Image _bonusImg;
    [SerializeField] private GameObject _parentBonus;
    [SerializeField] private Transform _endPos;
    
    [Header("--- Fade ---")]
    [SerializeField] private Image _fade;
    [SerializeField] private float _timeFadeOut = .5f;
    [SerializeField] private float _timeFadeIn = 1.5f;
    
    [Header("--- End ---")]
    [SerializeField] private TMP_Text _winText;
    [SerializeField] private float _timeSpawnWinText = 1;
    [SerializeField] private float _timeDespawnWinText = .5f;
    [SerializeField] private float _timeGoMainScene = 5;
    
    [Header("--- Hit ---")]
    [SerializeField] private Image _hit;
    [SerializeField] private float _timeHit = .25f;
    [SerializeField] private float _addShakeTime = .5f;
    [SerializeField] private float _addShakeStrength = 2f;
    
    [Header("--- Restart ---")]
    [SerializeField] private GameObject _restartBtn;

    private int _bonusCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _bonusTxt.text = $"{_bonusCount}";

        // Make Fade disappear & Bonus appear
        _fade.DOFade(0, _timeFadeOut*3);
        _bonusTxt.DOFade(1, _timeFadeOut);
        _bonusImg.DOFade(1, _timeFadeOut);
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
        // Juggle between fade & bonus to avoid having 2 bonus txt 
        _bonusTxt.DOFade(0, _timeFadeIn);
        _bonusImg.DOFade(0, _timeFadeIn);
        _fade.DOFade(1, _timeFadeIn);

        yield return new WaitForSeconds(_timeFadeIn);

        _parentBonus.transform.position = _endPos.position;
        _bonusTxt.DOFade(1, _timeSpawnWinText);
        _bonusImg.DOFade(1, _timeSpawnWinText);
        _winText.DOFade(1, _timeSpawnWinText);
        
        yield return new WaitForSeconds(_timeGoMainScene-_timeDespawnWinText);
        
        _winText.DOFade(0, _timeDespawnWinText);
        _bonusTxt.DOFade(0, _timeDespawnWinText);
        _bonusImg.DOFade(0, _timeDespawnWinText);
        
        yield return new WaitForSeconds(_timeDespawnWinText);
        
        SceneManager.LoadScene(0);
    }

    public void GetHit()
    {
        _hit.DOFade(1, 0);
        _hit.DOFade(0, _timeHit);
        Shaker.Instance.GoShakeAddPower(_addShakeTime, _addShakeStrength);
    }

    public void UpdateRestartBtn(bool state)
    {
        _restartBtn.transform.DOScale(state ? 1 : 0, .5f);
    }
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Spike : ModuleFeature
{
    [Header("--- Spike ---")]
    [SerializeField] private Image _spikeImg;
    [SerializeField] private bool _isSpikeAtStart;

    private int _count = 0;
    private int _startCount = 0;
    private bool _goDead = false;

    protected override void Start()
    {
        base.Start();

        if (!_isSpikeAtStart)
        {
            ChangeState();
            _startCount = 0;
        }
        else
        {
            _startCount = 1;
        }
    }

    protected override void ChangeState()
    {
        _count++;
        _spikeImg.gameObject.transform.DOComplete();
        
        if (_count > 1)
        {
            _count = 0;
            _goDead = true;

            _spikeImg.gameObject.transform.DOScale(Vector3.one, .5f);
        }
        else
        {
            _goDead = false;
            _spikeImg.gameObject.transform.DOScale(Vector3.one*.5f, .5f);
        }
    }
    
    public override void OnPlayerEnter()
    {
        if(_goDead)
            PlayerManager.Instance.KillPlayer();
    }

    protected override void ResetStartPos()
    {
        _count = _startCount;

        ChangeState();
    }
}
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Spike : ModuleFeature
{
    [Header("--- Spike ---")]
    [SerializeField] private SpriteRenderer _spriteSpike;
    [SerializeField] private bool _isSpikeAtStart;
    [SerializeField] private Sprite[] _sprites;

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
        
        if (_count >= _sprites.Length)
        {
            _count = 0;
            _goDead = true;
            AudioManager.Instance.PlaySound("snd_spikes_out");
        }
        else
        {
            _goDead = false;
        }

        
        _spriteSpike.sprite = _sprites[_count];
    }
    
    public override void OnPlayerEnter()
    {
        if (_goDead)
        {
            PlayerManager.Instance.KillPlayer();
            AudioManager.Instance.PlaySound("snd_death_spikes");
        }
    }

    protected override void ResetStartPos()
    {
        _count = _startCount;

        ChangeState();
    }
}
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LaserOnModule : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _sprites;

    private bool _isRotate;
    

    public void Init(Directions dir)
    {
        if (dir == Directions.Top || dir == Directions.Down)
        {
            _isRotate = true;
            _spriteRenderer.gameObject.transform.DORotate(new Vector3(0, 0, 90), 0);
        }
    }
    public void ChangeSprite(int index)
    {
        _spriteRenderer.sprite = _sprites[index];

        if (!_isRotate) return;
        
        if(index == _sprites.Length-1)
            _spriteRenderer.gameObject.transform.DORotate(new Vector3(0, 0, 0), 0);
        else
            _spriteRenderer.gameObject.transform.DORotate(new Vector3(0, 0, 90), 0);

    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LaserOnModule : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    // [SerializeField] private Module _myModule;
    // [SerializeField] private Module _isLaserOnTop;
    // [SerializeField] private Module _isLaserOnLeft;
    // [SerializeField] private Module _isLaserOnRight;
    // [SerializeField] private Module _isLaserOnDown;

    public void Init(Directions dir)
    {
        if (dir == Directions.Top || dir == Directions.Down)
            gameObject.transform.DORotate(new Vector3(0, 0, 90), 0);
    }
    public void ChangeSprite(Sprite newSprite)
    {
        _spriteRenderer.sprite = newSprite;
    }
}

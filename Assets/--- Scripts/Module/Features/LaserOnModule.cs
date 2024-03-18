using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserOnModule : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void ChangeSprite(Sprite newSprite)
    {
        _spriteRenderer.sprite = newSprite;
    }
}

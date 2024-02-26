using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;
    [SerializeField] private float _minSwipeDistance = 50f;

    [SerializeField] private Vector2 _minMaxTopSwipe;
    [SerializeField] private Vector2Int _minMaxLeftSwipe;
    [SerializeField] private Vector2Int _minMaxRightSwipe;
    [SerializeField] private Vector2Int _minMaxDownSwipe;
    
    
    private Vector2 _swipeStartPos;
    private Vector2 _swipeEndPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _swipeStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _swipeEndPos = Input.mousePosition;
            Vector2 swipeDirection = _swipeEndPos - _swipeStartPos;

            if (swipeDirection.magnitude >= _minSwipeDistance)
            {
                float angle = Mathf.Atan2(swipeDirection.y, swipeDirection.x) * Mathf.Rad2Deg;

                CheckDirection(angle);
                // Debug.Log("Swipe direction: " + angle);
            }
        }
    }

    private void CheckDirection(float angle)
    {
        if(angle > _minMaxRightSwipe.x && angle < _minMaxRightSwipe.y)
            _player.WantToSwipe(Directions.Right);
        else if(angle > _minMaxTopSwipe.x && angle < _minMaxTopSwipe.y)
            _player.WantToSwipe(Directions.Top);
        else if(angle < _minMaxLeftSwipe.x)
            _player.WantToSwipe(Directions.Left);
        else if(angle > _minMaxLeftSwipe.y)
            _player.WantToSwipe(Directions.Left);
        else if(angle > _minMaxDownSwipe.x && angle < _minMaxDownSwipe.y)
            _player.WantToSwipe(Directions.Down);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] float _swipeRange;
    [SerializeField] float _tapRange;
    Vector2 _startTouchPosition;
    Vector2 _currentPosition;
    Vector2 _endTouchPosition;
    bool _stopTouch;
    private void Update()
    {
        Swipe();
    }
    void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _currentPosition = Input.GetTouch(0).position;
            Vector2 distance = _currentPosition - _startTouchPosition;

            if (!_stopTouch)
            {

                if (distance.y > _swipeRange)
                {
                    //Swipe UP
                    EventManager.SwipeUp();
                    _stopTouch = true;
                }
                else if (distance.y < -_swipeRange)
                {
                    //Swipe Down
                    EventManager.SwipeDown();
                    _stopTouch = true;
                }
                else if (distance.x < -_swipeRange)
                {
                    //Swipe Left
                    EventManager.SwipeLeft();
                    _stopTouch = true;
                    print("swipe left");
                }
                else if (distance.x > _swipeRange)
                {
                    //Swipe Right
                    EventManager.SwipeRight();
                    _stopTouch = true;
                }
            }
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _stopTouch = false;

            _endTouchPosition = Input.GetTouch(0).position;

            Vector2 distance = _endTouchPosition = _startTouchPosition;

            if (Mathf.Abs(distance.x) < _tapRange && Mathf.Abs(distance.y) < _tapRange)
            {
                Debug.Log("Tap");
            }
        }
    }
}

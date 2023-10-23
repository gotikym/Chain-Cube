using UnityEngine;
using UnityEngine.Events;

public class SwipeDetector : MonoBehaviour
{
    public event UnityAction<Vector2> SwipeStarted;
    public event UnityAction<Vector2> Swiped;
    public event UnityAction<Vector2> SwipeEnded;

    private bool _isSwipe;
    private Vector3 _lastPosition = new Vector2();

    private void Update()
    {
        MouseSwipe();
        //TouchSwipe();
    }

    private void TouchSwipe()
    {
        if (Input.touchCount == 0)
        {
            if (_isSwipe)
            {
                _isSwipe = false;
                SwipeEnded?.Invoke(_lastPosition);
            }

            _lastPosition = Input.GetTouch(0).position;
            return;
        }
        else if (Input.touchCount > 0)
        {
            _isSwipe = true;
            SwipeStarted?.Invoke(Input.GetTouch(0).deltaPosition);
        }

        if (_isSwipe)
        {
            Swiped?.Invoke(Input.GetTouch(0).deltaPosition);
            _lastPosition = Input.GetTouch(0).position;
        }
    }

    private void MouseSwipe()
    {
        if (!Input.GetMouseButton(0))
        {
            if (_isSwipe)
            {
                _isSwipe = false;
                SwipeEnded?.Invoke(_lastPosition);
            }

            _lastPosition = Input.mousePosition;
            return;
        }

        if (!_isSwipe)
        {
            _isSwipe = true;
            SwipeStarted?.Invoke(Input.mousePosition - _lastPosition);
        }

        Swiped?.Invoke(Input.mousePosition - _lastPosition);
        _lastPosition = Input.mousePosition;
    }
}
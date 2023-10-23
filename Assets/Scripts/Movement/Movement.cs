using System;
using UnityEngine;

[RequireComponent(typeof(CubeSpawner), typeof(SwipeDetector))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private Transform _rightBorder;
    [SerializeField] private float _force = 1.0f;
    [SerializeField, Range(0.5f, 1.5f)] private float _normalizedCoefficient = 1.0f;

    private SwipeDetector _swipeDetector;
    private CubeSpawner _cubeSpawner;
    private GameObject _movableObject;
    private Rigidbody _movableRigidbody;

    private void Start()
    {
        _swipeDetector = GetComponent<SwipeDetector>();
        _cubeSpawner = GetComponent<CubeSpawner>();

        Subscribe();
    }

    private void Subscribe()
    {
        _swipeDetector.Swiped += OnSwiped;
        _swipeDetector.SwipeEnded += OnSwipeEnd;
        _cubeSpawner.CubeSpawned += OnCubeSpawn;
    }

    private void OnSwiped(Vector2 delta)
    {
        if (_movableObject == null)
            return;

        if (Mathf.Abs(delta.x - Mathf.Epsilon) <= 0)
            return;

        var borderDistance = Mathf.Abs(_rightBorder.position.x - _leftBorder.position.x);
        var offset = borderDistance * _normalizedCoefficient * delta.x / Screen.width;
        var currentPos = _movableObject.transform.position;

        _movableObject.transform.position = new Vector3(currentPos.x + offset, currentPos.y, currentPos.z);

        if (_movableObject.transform.position.x > _rightBorder.position.x)
            _movableObject.transform.position = _rightBorder.transform.position;
        else if (_movableObject.transform.position.x < _leftBorder.position.x)
            _movableObject.transform.position = _leftBorder.transform.position;
    }

    private void OnCubeSpawn(GameObject cube)
    {
        _movableRigidbody = cube.GetComponent<Rigidbody>();
        _movableObject = cube;
    }

    private void OnSwipeEnd(Vector2 delta)
    {
        _movableRigidbody.AddForce(_movableRigidbody.transform.forward * _force, ForceMode.Impulse);
        _movableObject = null;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Unsubscribe()
    {
        _swipeDetector.Swiped -= OnSwiped;
        _swipeDetector.SwipeEnded -= OnSwipeEnd;
        _cubeSpawner.CubeSpawned -= OnCubeSpawn;
    }
}
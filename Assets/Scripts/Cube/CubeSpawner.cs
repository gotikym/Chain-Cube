using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SwipeDetector))]
public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnDelay = 0.3f;
    [SerializeField] private GameObject _cubePrefab;

    private SwipeDetector _swipeDetector;
    private GameObject _cube;
    private Coroutine _spawnRoutine;

    public event UnityAction<GameObject> CubeSpawned;

    private void Start()
    {
        _swipeDetector = GetComponent<SwipeDetector>();

        _swipeDetector.SwipeEnded += OnSwipeEnd;

        InstantieateCube();
    }

    private void OnDestroy()
    {
        _swipeDetector.SwipeEnded -= OnSwipeEnd;
    }

    private void OnSwipeEnd(Vector2 delta)
    {
        if (_spawnRoutine == null)
            _spawnRoutine = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return null;
        yield return new WaitForSeconds(_spawnDelay);

        InstantieateCube();

        _spawnRoutine = null;
    }

    private void InstantieateCube()
    {
        _cube = Instantiate(_cubePrefab, transform.position, Quaternion.identity);
        CubeSpawned?.Invoke(_cube);
    }
}
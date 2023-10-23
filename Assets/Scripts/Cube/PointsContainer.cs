using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PointsContainer : MonoBehaviour
{
    [SerializeField] private uint _points;
    [SerializeField] private byte _minDegree = 1;
    [SerializeField] private byte _maxDegree = 4;

    private const byte DefaultMinDegree = 1;
    private const byte DefaultMaxDegree = 4;
    private const byte StartPoints = 2;

    public uint Points => _points;

    public event UnityAction<uint> PointsChanged;

    private void Start()
    {
        NormalizeDegree();

        SetRandomPoints();
    }

    private void NormalizeDegree()
    {
        if (_maxDegree > _minDegree) return;

        Debug.LogError("Установлены некорректные значения степени");

        _minDegree = DefaultMinDegree;
        _maxDegree = DefaultMaxDegree;
    }

    private void SetRandomPoints()
    {
        _points = (uint)Mathf.Pow(StartPoints, Random.Range(_minDegree, _maxDegree));
    }

    public void MultiplyPoints(byte multiplier)
    {
        _points *= multiplier;
        PointsChanged?.Invoke(_points);
    }
}
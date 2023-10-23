using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PointsContainer))]
public class MergePointsContainer : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PointsContainer _pointsContainer;

    private Vector3 _increaseCube  = new Vector3(1.35f, 1.35f, 1.35f);
    private Vector3 _standartCube = new Vector3(1f, 1f, 1f);
    private float _waitStandartScale = 0.75f;

    private const byte MultiplierPoints = 2;
    private const byte StrengthForce = 5;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _pointsContainer = GetComponent<PointsContainer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        var colContainer = collision.gameObject.GetComponent<PointsContainer>();

        if (colContainer == null)
            return;

        MergeСontainers(colContainer);
    }

    private void MergeСontainers(PointsContainer container)
    {
        if (container.Points == _pointsContainer.Points)
        {
            _pointsContainer.MultiplyPoints(MultiplierPoints);
            _rigidbody.AddForce(Vector3.up * StrengthForce, ForceMode.Impulse);

            StartCoroutine(ScaleCube());

            Destroy(container.gameObject);
        }
    }

    private IEnumerator ScaleCube()
    {
        _pointsContainer.gameObject.transform.localScale = _increaseCube;
        yield return new WaitForSeconds(_waitStandartScale);
        _pointsContainer.gameObject.transform.localScale = _standartCube;
    }
}
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PointsContainer))]
public class CubeView : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private TextMeshPro[] _texts;
    [SerializeField] private List<MaterialSettings> _settings = new List<MaterialSettings>();

    private PointsContainer _container;

    private void Start()
    {
        _container = GetComponent<PointsContainer>();

        _container.PointsChanged += OnPointsChanged;

        OnPointsChanged(_container.Points);
    }

    private void OnPointsChanged(uint points)
    {
        foreach (var text in _texts)
        {
            text.text = points.ToString();
            var settings = _settings.Find(t => t.points == points);

            if (settings == null)
                _renderer.material = default;
            else
                _renderer.material = settings.material;
        }
    }

    private void OnDestroy()
    {
        _container.PointsChanged -= OnPointsChanged;
    }
}

[System.Serializable]
public class MaterialSettings
{
    public long points;
    public Material material;
}
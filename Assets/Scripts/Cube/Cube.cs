using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _intersections = 0;

    private int _intersectionsToLose = 2;

    public static UnityAction Losed;

    public void InterectionLoseZone()
    {
        _intersections++;

        if (_intersections == _intersectionsToLose)
            Losed?.Invoke();
    }
}

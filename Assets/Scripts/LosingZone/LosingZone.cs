using UnityEngine;
using UnityEngine.Events;

public class LosingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube))
            cube.InterectionLoseZone();
    }
}

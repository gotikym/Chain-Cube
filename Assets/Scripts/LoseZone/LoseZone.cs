using UnityEngine;
using UnityEngine.Events;

public class LoseZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube))
            cube.InterectionLoseZone();
    }
}

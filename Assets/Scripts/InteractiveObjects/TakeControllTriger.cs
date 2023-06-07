using UnityEngine;

public class TakeControllTriger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            controller.ClimbingStart();
        }
    }
}
using UnityEngine;

public class ClimbableObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            controller.ClimbingStart();
            // controller.MoveTo(this.transform.position.x);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            controller.ClimbingEnd();
        }
    }
}

using UnityEngine;

public class ChangerLvlTrigger : MonoBehaviour
{
    [SerializeField] private string _lvl;
    [SerializeField] private GameObject _lvlFirstColliders;
    [SerializeField] private GameObject _lvlFirstObjectWithColliders;
    [SerializeField] private GameObject _lvlSecondColliders;
    [SerializeField] private GameObject _lvlSecondObjectWithColliders;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            controller.ChangeLayer(_lvl);
            _lvlFirstColliders.SetActive(false);
            SetActiveColliders(_lvlFirstObjectWithColliders, false);
            _lvlSecondColliders.SetActive(true);
            SetActiveColliders(_lvlSecondObjectWithColliders, true);
        }
    }

    private void SetActiveColliders(GameObject gameObject, bool activeMode)
    {
        if (gameObject.TryGetComponent(out Collider2D collider)) collider.enabled = activeMode;

        var childCollider = gameObject.GetComponentsInChildren<Collider2D>();
        foreach (var collider2D1 in childCollider) collider2D1.enabled = activeMode;
    }
}

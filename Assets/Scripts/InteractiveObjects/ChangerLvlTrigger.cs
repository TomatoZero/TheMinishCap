using UnityEngine;
using UnityEngine.Serialization;

public class ChangerLvlTrigger : MonoBehaviour
{
    [SerializeField] private string _lvlFrom;
    [SerializeField] private GameObject _lvlFromColliders;
    [SerializeField] private GameObject _lvlFromObjectWithColliders;
    [SerializeField] private GameObject _lvlToColliders;
    [SerializeField] private GameObject _lvlToObjectWithColliders;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            controller.ChangeLayer(_lvlFrom);
            _lvlToColliders.SetActive(false);
            SetActiveColliders(_lvlToObjectWithColliders, false);
            _lvlFromColliders.SetActive(true);
            SetActiveColliders(_lvlFromObjectWithColliders, true);
        }
    }

    private void SetActiveColliders(GameObject gameObject, bool activeMode)
    {
        if (gameObject.TryGetComponent(out Collider2D collider)) collider.enabled = activeMode;

        var childCollider = gameObject.GetComponentsInChildren<Collider2D>();
        foreach (var collider2D1 in childCollider) collider2D1.enabled = activeMode;
    }
}

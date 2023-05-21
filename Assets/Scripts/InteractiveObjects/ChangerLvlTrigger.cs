using System;
using UnityEngine;

public class ChangerLvlTrigger : MonoBehaviour
{
    [SerializeField] private string _lvl;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            controller.ChangeLayer(_lvl);
        }
    }
}

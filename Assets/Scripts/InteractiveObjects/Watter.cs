using System;
using UnityEngine;

public class Watter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out CharacterController player))
        {
            Debug.Log($"the player has sunk");
        }
    }
}

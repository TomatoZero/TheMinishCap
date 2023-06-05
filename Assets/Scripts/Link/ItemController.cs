using System;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private PlayerHpEvent _healPickUp;
    [SerializeField] private PlayerHpEvent _heartContainerPickUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Heal"))
        {
            if (other.TryGetComponent(out Item item))
            {
                var heal = item.Data as HealthData;
                if (heal != null)
                {
                    _healPickUp.Invoke(heal.NumbHealthRestore);
                    item.PicUp();
                    Debug.Log($"Heal pick up {heal.NumbHealthRestore}");
                }
                else throw new Exception($"Heal Data null");
            }
            else throw new Exception("Item script missing");
        }
        else if(other.CompareTag("HeartContainer"))
        {
            if (other.TryGetComponent(out Item item))
            {
                var healContainer = item.Data as HeartContainerData;
                if (healContainer != null)
                {
                    _heartContainerPickUp.Invoke(healContainer.NumbHeartbeat);
                    item.PicUp();
                    Debug.Log($"Heart pick up {healContainer.NumbHeartbeat}");
                }
                else throw new Exception($"HealContainer Data null");

            }
        }
    }
}

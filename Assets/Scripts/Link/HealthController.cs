using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField, Min(1)] private int _baseHp;

    private int _currentHp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        throw new NotImplementedException();
    }
}

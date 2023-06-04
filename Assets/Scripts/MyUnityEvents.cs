using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SnakeAttackEvent : UnityEvent<Vector2>
{
}

[Serializable]
public class PlayerTakeDamageEvent : UnityEvent<int>
{
}

[Serializable]
public class PlayerHpEvent : UnityEvent<int>
{
}

[Serializable]
public class ChangeDirectionViewEvent : UnityEvent<Vector2>
{
}
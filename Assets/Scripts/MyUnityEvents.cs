using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SnakeAttackEvent : UnityEvent<Vector2>
{
}

[Serializable]
public class PlayerTakeDamageEvent : UnityEvent<int, State, Vector2>
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

[Serializable]
public class FallEvent : UnityEvent<State>
{
}

[Serializable]
public class ClimbEvent : UnityEvent<bool>
{
}
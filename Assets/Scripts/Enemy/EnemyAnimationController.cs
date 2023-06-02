using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _side;
    [SerializeField] private Sprite _front;
    [SerializeField] private Sprite _back;

    public void SetSprite(Vector2 direction)
    {
        _spriteRenderer.flipX = false;
        if (direction == Vector2.down) _spriteRenderer.sprite = _front;
        else if(direction == Vector2.left) _spriteRenderer.sprite = _side;
        else if (direction == Vector2.right)
        {
            _spriteRenderer.sprite = _side;
            _spriteRenderer.flipX = true;
        }
        else if (direction == Vector2.up) _spriteRenderer.sprite = _back;
    }
}

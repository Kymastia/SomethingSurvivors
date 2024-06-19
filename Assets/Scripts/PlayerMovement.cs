using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Min(0)]
    private float _speed = 1.0f;

    [SerializeField]
    private SpriteRenderer _sprite;

    private bool _spriteFlipY;
    private Rigidbody2D _rigidbody;
    private Vector2 _movementDirection = new();

    public float Speed
    {
        get => _speed;
        set
        {
            Debug.Assert(value >= 0, "Speed must be a positive number.", this);
            _speed = value;
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_sprite)
        {
            _spriteFlipY = _sprite.flipY;
        }
    }

    private void OnValidate()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (!_rigidbody.isKinematic)
        {
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            Debug.Log(_rigidbody.name + " automatically set to kinematic.", _rigidbody);
        }

        if (!_sprite)
        {
            _sprite = GetComponentInChildren<SpriteRenderer>(true);
        }
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        _movementDirection.Set(x, y);
        _movementDirection.Normalize();

        if (_sprite && x != 0)
        {
            bool isFacingBackwards = x < 0;
            _sprite.flipY = isFacingBackwards ^ _spriteFlipY;
        }
    }

    private void FixedUpdate()
    {
        var newPosition = _rigidbody.position + _movementDirection * (Speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);
    }
}

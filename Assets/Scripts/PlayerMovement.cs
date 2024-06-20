using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.5f;

    [SerializeField]
    private SpriteRenderer _sprite;

    private bool _spriteFlipY;
    private Rigidbody2D _rigidbody;
    private Vector2 _inputDirection;
    private Locomotion _locomotion;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _locomotion = new(
            _speed,
            () => _rigidbody.position,
            _rigidbody.MovePosition
        );

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

        if (_locomotion is not null)
        {
            _locomotion.Speed = _speed;
        }
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        _inputDirection.Set(x, y);
        _inputDirection.Normalize();

        if (_sprite && x != 0)
        {
            bool isFacingBackwards = x < 0;
            _sprite.flipY = isFacingBackwards ^ _spriteFlipY;
        }
    }

    private void FixedUpdate()
    {
        _locomotion.MoveDirection(_inputDirection, Time.fixedDeltaTime);
    }
}

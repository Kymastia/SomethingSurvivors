using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField, Min(0)]
    public float Speed { get; set; } = 1.0f;

    [SerializeField]
    private SpriteRenderer _sprite;
    private bool _spriteFlipY;

    private Rigidbody2D Rigidbody;
    private Vector2 MovementDirection = new();

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();

        if (_sprite)
        {
            _spriteFlipY = _sprite.flipY;
        }
    }

    private void OnValidate()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        if (!Rigidbody.isKinematic)
        {
            Rigidbody.bodyType = RigidbodyType2D.Kinematic;
            Debug.Log(Rigidbody.name + " automatically set to kinematic.", Rigidbody);
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
        MovementDirection.Set(x, y);

        if (_sprite && x != 0)
        {
            bool isFacingBackwards = x < 0;
            _sprite.flipY = isFacingBackwards ^ _spriteFlipY;
        }
    }

    private void FixedUpdate()
    {
        var newPosition = Rigidbody.position + MovementDirection * (Speed * Time.fixedDeltaTime);
        Rigidbody.MovePosition(newPosition);
    }
}

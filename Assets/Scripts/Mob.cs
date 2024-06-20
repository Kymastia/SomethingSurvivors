using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField, Min(0)]
    private float _speed = 1.0f;

    private Transform _target;

    public float Speed
    {
        get => _speed;
        set
        {
            Debug.Assert(value >= 0, "Speed must be a positive number.", this);
            _speed = value;
        }
    }

    public void SetTarget(Transform transform)
    {
        _target = transform;
    }

    public void FixedUpdate()
    {
        if (!_target)
        {
            return;
        }

        var position = Vector3.MoveTowards(
            transform.position,
            _target.position,
            Speed * Time.fixedDeltaTime
        );

        transform.position = position;
    }

    [ContextMenu("Chase Player")]
    private void ChasePlayer()
    {
        var player = FindAnyObjectByType<PlayerMovement>();
        if (!player) return;
        SetTarget(player.transform);
    }
}

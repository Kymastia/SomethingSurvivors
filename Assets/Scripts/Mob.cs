using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField, Min(0)]
    private float _speed = 1.0f;

    private Transform _target;
    private Locomotion _locomotion;

    private void Awake()
    {
        _locomotion = new(
            _speed,
            () => transform.position,
            position => transform.position = position
        );
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

        _locomotion.MoveTowards(_target.position, Time.fixedDeltaTime);
    }

    [ContextMenu("Chase Player")]
    private void ChasePlayer()
    {
        var player = FindAnyObjectByType<PlayerMovement>();
        if (!player) return;
        SetTarget(player.transform);
    }
}

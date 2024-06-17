using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Camera))]
public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _focusTarget;

    [SerializeField, Min(0)]
    private float _maxDistance = 0.5f;

    [SerializeField, Min(0)]
    private float _smoothTime = 0.5f;

    public Transform FocusTarget {
        get => _focusTarget;
        set {
            enabled = value;
            _focusTarget = value;
        }
    }

    public float MaxDistance {
        get => _maxDistance;
        set
        {
            Debug.Assert(value >= 0, "Distance must be a positive value.", this);
            _maxDistance = value;
        }
    }

    public float SmoothTime {
        get => _smoothTime;
        set
        {
            Debug.Assert(value >= 0, "Time must be a positive value.", this);
            _smoothTime = value;
        }
    }

    private Vector2 _velocity;

    private void Awake()
    {
        enabled = _focusTarget;
    }

    private Vector2 CalculateNextDestination()
    {
        Vector2 current = transform.position;
        Vector2 target = FocusTarget.position;
        Vector2 currentToTarget = target - current;

        if (Vector2.SqrMagnitude(currentToTarget) > MaxDistance * MaxDistance)
        {
            var delta = Vector2.ClampMagnitude(currentToTarget, MaxDistance);
            return target - delta;
        }
        else
        {
            return Vector2.SmoothDamp(current, target, ref _velocity, _smoothTime);
        }
    }

    private void LateUpdate()
    {
        Vector2 target = _focusTarget.position;
        Vector2 current = transform.position;
        Vector2 difference = current - target;

        bool isTargetTooFar = Vector2.SqrMagnitude(difference) > MaxDistance * MaxDistance;

        var destination = CalculateNextDestination();

        var newPosition = transform.position;
        newPosition.x = destination.x;
        newPosition.y = destination.y;
        transform.position = newPosition;
    }
}

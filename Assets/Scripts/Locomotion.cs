using System;
using UnityEngine;

public class Locomotion
{
    private float _speed = 1.0f;

    public Vector2 Direction { get; private set; }

    public float Speed
    {
        get => _speed;
        set
        {
            if (_speed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Speed), value, "Speed must be a positive number.");
            }

            _speed = value;
        }
    }


    private readonly Func<Vector2> _positionGetter;
    private readonly Action<Vector2> _positionSetter;

    public Locomotion(float speed, Func<Vector2> positionGetter, Action<Vector2> positionSetter)
    {
        Speed = speed;
        _positionGetter = positionGetter;
        _positionSetter = positionSetter;
    }

    public void MoveTowards(Vector2 target, float deltaTime)
    {
        var position = _positionGetter();
        var direction = target - position;
        MoveDirection(direction, deltaTime);
    }

    public void MoveDirection(Vector2 direction, float deltaTime)
    {
        direction.Normalize();
        Direction = direction;

        var position = _positionGetter() + Direction * (Speed * deltaTime);
        _positionSetter(position);
    }
}

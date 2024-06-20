using System;
using UnityEngine;


[Serializable]
public struct FloatRange
{

    [SerializeField]
    private float _value;

    private float _min, _max;

    public FloatRange(float value, float min, float max)
    {
        Debug.Assert(min <= max, "Min must be less than max.");
        Debug.Assert(min <= value && value <= max, "Value out of bounds.");

        _value = value;
        _min = min;
        _max = max;
    }

    public readonly float Min => _min;

    public readonly float Max => _max;

    public float Value
    {
        readonly get => _value;
        set
        {
            Debug.Assert(Min <= value && value <= Max, "Value out of bounds.");
            _value = value;
        }
    }

    public override readonly bool Equals(object obj)
    {
        return obj is FloatRange range && this == range;
    }

    public override readonly int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(in FloatRange left, in FloatRange right)
    {
        return left.Value == right.Value;
    }

    public static bool operator ==(in FloatRange left, float right)
    {
        return left.Value == right;
    }

    public static bool operator !=(in FloatRange left, in FloatRange right)
    {
        return !(left == right);
    }

    public static bool operator !=(in FloatRange left, float right)
    {
        return !(left == right);
    }

    public static implicit operator float(FloatRange value) => value.Value;
}

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FloatRange))]
public class FloatRangeEditor : PropertyDrawer
{
    private const int Rows = 2;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) * Rows;
    }

    public override void OnGUI(Rect totalArea, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(totalArea, label, property);

        var value = property.FindPropertyRelative("_value");

        object obj = property.serializedObject.targetObject;
        var field = obj.GetType().GetField("_speed", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var range = (FloatRange)field.GetValue(property.serializedObject.targetObject);

        float rowHeight = totalArea.height / Rows;

        var labelRect = totalArea;
        labelRect.height = rowHeight;

        EditorGUI.PrefixLabel(labelRect, GUIUtility.GetControlID(FocusType.Passive), GUIContent.none);
        EditorGUI.Slider(labelRect, value, range.Min, range.Max);
        EditorGUI.PropertyField(labelRect, value);

        labelRect.y += rowHeight;
        EditorGUI.LabelField(labelRect, " ", $"Range: [{range.Min:0.00}, {range.Max:0.00}]");

        EditorGUI.EndProperty();
    }
}

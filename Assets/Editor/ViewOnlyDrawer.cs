using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ViewOnlyAttribute))]
public class ViewOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        var obj = property.serializedObject.targetObject;
        var field = obj.GetType().GetField(
            property.name,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
        );

        Debug.Log(field.Name);

        EditorGUI.LabelField(position, label.text, field.GetValue(obj).ToString());
        EditorGUI.EndProperty();
    }
}

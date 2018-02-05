using System;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Creates a dropdown menu to choose a layer from.
/// </summary>
[CustomPropertyDrawer(typeof(LayerAttribute))]
public class LayerPropertyDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        Debug.Assert(property.propertyType == SerializedPropertyType.Integer, "Only integers supported!");

        EditorGUI.BeginProperty(position, label, property);
        property.intValue = EditorGUI.LayerField(position, label, property.intValue);
        EditorGUI.EndProperty();
    }

}

using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(NonNegativeAttribute))]
public class NonNegativeDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

        EditorGUI.PropertyField(position, property);

        if(property.propertyType == SerializedPropertyType.Float) {
            property.floatValue = Mathf.Max(0, property.floatValue);
        } else if (property.propertyType == SerializedPropertyType.Integer) {
            property.intValue = Mathf.Max(0, property.intValue);
        } else {
            EditorGUI.LabelField(position, "NonNegative is only applicable to float and int.");
        }
    }

}
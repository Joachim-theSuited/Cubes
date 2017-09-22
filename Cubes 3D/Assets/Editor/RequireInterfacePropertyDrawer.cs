using System;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Property drawer for the RequireInterface attribute. Ensures that a referenced MonoBehaviour implements a given interface.
/// </summary>
[CustomPropertyDrawer(typeof(RequireInterface))]
public class RequireInterfacePropertyDrawer : PropertyDrawer {
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        // the editor just lets you choose a GameObject and picks a MonoBehaviour
        EditorGUI.ObjectField(position, property);
        // if that has not the required type, try to find another Component that has it, else clear the field (if GetComponent returns null)
        RequireInterface iface = attribute as RequireInterface;
        if(property.objectReferenceValue != null && !iface.type.IsInstanceOfType(property.objectReferenceValue)) {
            property.objectReferenceValue = (property.objectReferenceValue as MonoBehaviour).gameObject.GetComponent(iface.type);
        }
    }

}
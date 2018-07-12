using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SceneAttribute))]
public class SceneAttributeDrawer : PropertyDrawer {

    int lastMenuSelection = -1;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

        // determine scene index from property
        int selectedSceneID = -1;
        if(property.propertyType == SerializedPropertyType.String) {
            for(int buildID = 0; buildID < EditorBuildSettings.scenes.Length; ++buildID) {
                if(EditorBuildSettings.scenes[buildID].path == property.stringValue) {
                    selectedSceneID = buildID;
                }
            }
        } else if(property.propertyType == SerializedPropertyType.Integer) {
            selectedSceneID = property.intValue;
        } else {
            EditorGUI.LabelField(position, "SceneAttribute is only applicable to int or string.");
            return;
        }

        EditorGUI.BeginProperty(position, label, property);
           // try to apply the last menu selection
            if(lastMenuSelection >= 0 && lastMenuSelection < EditorBuildSettings.scenes.Length) {
                selectedSceneID = lastMenuSelection;
                if(property.propertyType == SerializedPropertyType.String) {
                    property.stringValue = EditorBuildSettings.scenes[lastMenuSelection].path;
                } else if(property.propertyType == SerializedPropertyType.Integer) {
                    property.intValue = lastMenuSelection;
                }
            }
            lastMenuSelection = -1; // reset

            // get scene name
            string selectedSceneName = SceneNameFromID(selectedSceneID);

            // display
            EditorGUI.LabelField(position, label);

            position.xMin += EditorGUIUtility.labelWidth;

            if(EditorGUI.DropdownButton(position, new GUIContent(selectedSceneName), FocusType.Keyboard)) {
                GenericMenu menu = new GenericMenu();
                for(int buildID = 0; buildID < EditorBuildSettings.scenes.Length; ++buildID) {
                    var scene = EditorBuildSettings.scenes[buildID];
                    bool isSelected = ( buildID == selectedSceneID );
                    menu.AddItem(new GUIContent(SceneNameFromID(buildID), scene.path), isSelected, SelectID, buildID);
                }
                menu.DropDown(position);
            }
        EditorGUI.EndProperty();
    }

    // menu callback
    void SelectID(object id) {
        lastMenuSelection = (int) id;
    }

    // extracts file name from path
    string SceneNameFromID(int sceneID) {
        if(sceneID < 0 || sceneID >= EditorBuildSettings.scenes.Length) {
            return "<Invalid>";
        }

        var scene = EditorBuildSettings.scenes[sceneID];
        var pathEnd = scene.path.LastIndexOf("/") + 1;
        var nameEnd = scene.path.LastIndexOf(".");
        return scene.path.Substring(pathEnd, nameEnd-pathEnd);
    }

}
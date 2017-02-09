using UnityEngine;
using UnityEditor;

/// <summary>
/// User interface utility functions for use in the editor.
/// </summary>
public static class UIUtility {

    public delegate void CallBack();

    /// <summary>
    /// Creates a standard button with the specified name.
    /// When the button is clicked, the callback will be invoked.
    /// </summary>
    public static void SimpleButton(string name, CallBack function) {
        if(Button(name)) {
            function.Invoke();
        }
    }

    /// <summary>
    /// Creates a Button with the specified name.
    /// Returns whether the Button was clicked.
    /// </summary>
    public static bool Button(string name) {
        GUIContent content = new GUIContent(name);
        GUIStyle style = new GUIStyle("Button");
        Rect r = GUILayoutUtility.GetRect(content, style);
        return GUI.Button(r, content);
    }

    /// <summary>
    /// Creates standardised Buttons for an IBuilder.
    /// The Buttons are 'Rebuild', 'Clear' and 'Build'.
    /// </summary>
    public static void BuilderButtons(IBuilder[] targets) {
        CallBack build = () => BuildAll(targets);
        CallBack clear = () => ClearAll(targets);

        GUILayout.BeginHorizontal();
        UIUtility.SimpleButton("Rebuild", clear+build);
        UIUtility.SimpleButton("Clear", clear);
        UIUtility.SimpleButton("Build", build);
        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// Overloaded version for a single target.
    /// </summary>
    public static void BuilderButtons(IBuilder target) {
        BuilderButtons(new[] {target});
    }

    static void BuildAll(IBuilder[] targets) {
        foreach(IBuilder builder in targets) {
            builder.Build();
        }
    }

    static void ClearAll(IBuilder[] targets) {
        foreach(IBuilder builder in targets) {
            Transform transform = builder.GetTransform();
            if(transform != null) {
                while(transform.childCount != 0) {
                    Object.DestroyImmediate(transform.GetChild(0).gameObject);
                }
            }
        }
    }

    static Vector2 handleSizeVec = 7 * Vector2.one;
    /// <summary>
    /// The size of the handle in px.
    /// Will be clamped to [1, inf].
    /// </summary>
    public static float handleSize {
        get {return handleSizeVec.x;}
        set {handleSizeVec = Mathf.Max(1, value) * Vector2.one;}
    }

    /// <summary>
    /// Draws a handle in the GUI (Applies Handles.color).
    /// The handle's position is considered relative to the bounding Rect.
    /// If it is editable, the user can click or drag with the mouse to move the handle.
    /// Mouse positions outside of the given bounding Rect will be ignored.
    /// Returns the new position.
    /// </summary>
    public static Vector2 Handle2D(Vector2 currentPos, Rect boundingRect, bool editable) {
        Vector2 handlePos = boundingRect.position + currentPos;
        EditorGUI.DrawRect(new Rect(handlePos - handleSizeVec / 2, handleSizeVec), Handles.color);
        if(editable) {
            if((Event.current.type == EventType.MouseDown || Event.current.type == EventType.MouseDrag)
                && boundingRect.Contains(Event.current.mousePosition)) {
                return Event.current.mousePosition - (boundingRect.center - boundingRect.size / 2);
            }
        }
        return currentPos;
    }

    /// <summary>
    /// Overloaded version that applies changes directly to a SerializedProperty.
    /// No-Op for SerializedProperties that aren't of type Vector2.
    /// </summary>
    public static void Handle2D(SerializedProperty vec2, Rect boundingRect, int scale, bool editable) {
        if(vec2.type == SerializedPropertyType.Vector2.ToString()){
            vec2.vector2Value = Handle2D(vec2.vector2Value*scale, boundingRect, editable)/scale;
        }
    }

}

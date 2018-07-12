using System;
using UnityEngine;

/// <summary>
/// Use this attribute on an int or string property to display a dropdown with the scenes included in the build.
/// Int properties will store the index of the scene from the BuildSettings.
/// String properties will store the scene asset path.
/// </summary>
public class SceneAttribute : PropertyAttribute {
    // empty
}

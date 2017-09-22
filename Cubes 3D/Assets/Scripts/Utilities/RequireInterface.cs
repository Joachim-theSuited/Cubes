using System;
using UnityEngine;

/// <summary>
/// Use this attribute on a MonoBehaviour property to enforce that the referenced behaviour implements a certain interface.
/// </summary>
public class RequireInterface : PropertyAttribute {

    public Type type{ get; private set; }

    /// <param name="type">Type of the interface (or class) that is required.</param>
    public RequireInterface(Type type) {
        this.type = type;
    }

}

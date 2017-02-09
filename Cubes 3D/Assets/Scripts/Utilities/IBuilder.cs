using UnityEngine;

/// <summary>
/// Interface for editor scripts that build objects into the scene.
/// </summary>
public interface IBuilder {

    /// <summary>
    /// Place the object(s) into the scene.
    /// Usually all the objects should be gathered under one parent Transform.
    /// </summary>
    void Build();

    /// <summary>
    /// Gets the transform under which the objects where created.
    /// This is used to enable clearing of the builder by deleting all children of the Transform.
    /// May return null.
    /// </summary>
    Transform GetTransform();

}

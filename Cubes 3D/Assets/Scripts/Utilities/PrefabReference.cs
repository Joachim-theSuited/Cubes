using System;
using UnityEngine;

/// <summary>
/// Let some component in a prefab hierarchy contains a reference to the prefab itself.
/// If you instantiate this prefab, the reference will then point to the corresponding part of the new instance.
/// The reference to the prefab will be lost.
/// This container provides a wrapper so that a prefab can keep a reference to itself, even after instantiation.
/// </summary>
[CreateAssetMenu(fileName = "MyRef.asset", menuName = "Custom/Prefab Ref", order = 0)]
public class PrefabReference : ScriptableObject {
    
    public GameObject reference;

}

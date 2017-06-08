using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple shield, that is raised on use.
/// </summary>
public class Shield : DummyItem {

    public void ReceiveDamage(float dam) {
        Debug.Log(dam + " damage blocked");
    }

}

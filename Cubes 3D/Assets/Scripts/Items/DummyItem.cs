using UnityEngine;
using System.Collections;

public class DummyItem : ItemScript {

    bool inUse = false;
    Coroutine coroutine;

    public override void Use(GameObject player) {
        coroutine = StartCoroutine(UseEquipped());
    }

    public override void OnDropped(GameObject player) {
        base.OnDropped(player);
        if(coroutine != null)
            StopCoroutine(coroutine);
        inUse = false;
    }

    IEnumerator UseEquipped() {
        if(!inUse) {
            inUse = true;
            transform.localPosition += Vector3.up;
            yield return new WaitForSeconds(1);
            transform.localPosition += Vector3.down;
            inUse = false;
        }
    }

}

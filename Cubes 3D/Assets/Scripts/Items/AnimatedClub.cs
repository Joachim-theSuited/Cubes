using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple weapon-item with its own animation.
/// </summary>
public class AnimatedClub : ItemScript {

    public float damage = 1;
    public float duration = 1;

    void OnCollisionEnter(Collision coll) {
        if(isSwinging)
            coll.gameObject.SendMessageUpwards(Messages.DAMAGE, damage, SendMessageOptions.DontRequireReceiver);
    }

    bool isSwinging = false;

    public override void Use(GameObject player) {
        base.Use(player);
        if(!isSwinging) {
            isSwinging = true;
            StartCoroutine(swing());
        }
    }

    IEnumerator swing() {
        TimeLerper<Quaternion> quatLerp = new TimeLerper<Quaternion>(Quaternion.Lerp, transform.localRotation, Quaternion.LookRotation(Vector3.down, Vector3.forward), duration);

        while(quatLerp.isNotDone) {
            yield return new WaitForFixedUpdate();
            transform.localRotation = quatLerp.Current();
        } 

        quatLerp = quatLerp.GetInverse();
        while(quatLerp.isNotDone) {
            yield return new WaitForFixedUpdate();
            transform.localRotation = quatLerp.Current();
        }

        isSwinging = false;
    }

}

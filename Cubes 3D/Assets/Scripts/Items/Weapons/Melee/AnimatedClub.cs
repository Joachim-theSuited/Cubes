using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple weapon-item with its own animation.
/// </summary>
public class AnimatedClub : ItemScript {

    public float damage = 1;
    public float duration = 1;

    List<Rigidbody> hits;

    void Awake() {
        hits = new List<Rigidbody>();
    }

    void OnCollisionEnter(Collision coll) {
		if(isSwinging && !hits.Contains(coll.rigidbody) && coll.gameObject.GetComponent<AbstractDamageReceiver>() != null) {
            try {
                //check if the collider should be hit directly
                //RequireReceiver, to cause an Exception
                coll.collider.SendMessage(Messages.DAMAGE, damage, SendMessageOptions.RequireReceiver);
            } catch(UnityException e) {
                //if no direct hit was triggered, send to main rigidbody
                coll.rigidbody.SendMessage(Messages.DAMAGE, damage, SendMessageOptions.DontRequireReceiver);
            }

            hits.Add(coll.rigidbody);
        }
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
        hits.Clear();
        isSwinging = false;
    }

}

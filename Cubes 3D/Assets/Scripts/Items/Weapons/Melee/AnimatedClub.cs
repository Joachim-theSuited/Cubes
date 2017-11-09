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
        if(isSwinging && !hits.Contains(coll.rigidbody)) {
            var damageReciever = coll.collider.GetComponentInParent<AbstractDamageReceiver>();
            if(damageReciever != null)
                //using this.gameObject directly is problematic, when the weapon is above the target
                //so we fall back onto the rigidbody - and hope there is one
                damageReciever.ReceiveDamage(new DamageInfo(damage, GetComponentInParent<Rigidbody>().gameObject));
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

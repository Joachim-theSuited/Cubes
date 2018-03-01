using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that kills the player, when they are below the height of the attached GameObject.
/// </summary>
public class KillingFloor : MonoBehaviour {

    GameObject player;

    void Start() {
        player = GameObject.FindWithTag(Tags.Player);
    }

    void Update() {
        if(player.transform.position.y <= transform.position.y) {
            var hp = player.GetComponent<HitPoints>();
            if(hp) {
                hp.ReceiveDamage(new DamageInfo(hp.maxHealth, this.gameObject));
            } else {
                Destroy(player);
            }
            // script shouldn't be needed a second time
            // (also, damaging the dead player spawns additional corpses ...)
            Destroy(this);
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(100000, 0, 100000));
    }

}

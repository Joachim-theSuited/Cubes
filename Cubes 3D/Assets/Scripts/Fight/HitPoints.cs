using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : AbstractDamageReceiver {

    /// <summary>
    /// The initial/maximum health.
    /// </summary>
    public float maxHealth;

    /// <summary>
    /// The GameObject to spawn instead of this one when the hitpoints reach 0.
    /// </summary>
    public GameObject corpse;

    float currentHealth;

    void Start() {
        currentHealth = maxHealth;
    }

    public override void ReceiveDamage(float dam) {
        currentHealth -= dam;
        Debug.Log(name + " health now at " + currentHealth);
        if(currentHealth <= 0) {
            gameObject.SetActive(false);
            Instantiate(corpse, transform.position, Quaternion.identity);
        }
    }

}

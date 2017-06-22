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
            if(gameObject.tag != Tags.Player)
                Destroy(gameObject);
            else // the camera depends on the player object, so it is only deactivated
                gameObject.SetActive(false);
            
            if(corpse != null)
                Instantiate(corpse, transform.position, Quaternion.identity);
        }
    }

}

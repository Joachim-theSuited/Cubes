using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoints : MonoBehaviour, AbstractDamageReceiver, IExpressibleAsFraction {

    /// <summary>
    /// The initial/maximum health.
    /// </summary>
    public float maxHealth;

    /// <summary>
    /// The GameObject to spawn instead of this one when the hitpoints reach 0.
    /// </summary>
    public GameObject corpse;


    public float knockBack;
    public bool knockBackScalesWithDamage;

    float currentHealth;

    void Start() {
        currentHealth = maxHealth;
    }

    public void ReceiveDamage(float dam, GameObject source) {
        currentHealth -= dam;
        Debug.Log(name + " health now at " + currentHealth);
        if(currentHealth <= 0) {
            if(gameObject.tag != Tags.Player)
                Destroy(gameObject);
            else // the camera depends on the player object, so it is only deactivated
                gameObject.SetActive(false);
            
            if(corpse != null)
                Instantiate(corpse, transform.position, Quaternion.identity);
        } else {
            var rb = GetComponent<Rigidbody>();
            if(rb != null) {
                var horizontalDirection = transform.position - source.transform.position;
                horizontalDirection.y = 0;
                rb.AddForce(horizontalDirection.normalized * knockBack, ForceMode.VelocityChange);
            }
        }
    }

    float IExpressibleAsFraction.GetFraction() {
        return currentHealth / maxHealth;
    }

}

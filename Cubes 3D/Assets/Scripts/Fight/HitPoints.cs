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

    /// <summary>
    /// The animation trigger to set upon receiving damage.
    /// </summary>
    public string hitAnimationTrigger;
    Animator animator;

    public float knockBack;
    public bool knockBackScalesWithDamage;

    float currentHealth;

    void Start() {
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
    }

    public void ReceiveDamage(DamageInfo di) {
        currentHealth -= di.damageAmount;
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
                var horizontalDirection = transform.position - di.source.transform.position;
                horizontalDirection.y = 0;
                rb.AddForce(horizontalDirection.normalized * knockBack, ForceMode.VelocityChange);
            }
            if(animator != null && hitAnimationTrigger != "") {
                animator.SetTrigger(hitAnimationTrigger);
            }
        }
    }

    float IExpressibleAsFraction.GetFraction() {
        return currentHealth / maxHealth;
    }

}

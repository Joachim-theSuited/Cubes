using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControls))]
public class PlayerSprintBehaviour : MonoBehaviour, IExpressibleAsFraction {

    /// How much faster than normal movement is sprinting?
    public float speedup;

    /// Maximum stamina
    public float maxStamina;
    /// Stamina usage per second, when sprinting
    public float staminaConsumptionPerSec = 1;
    /// Stamina recovery, when not sprinting
    public float staminaRecoveryPerSec = 2;

    /// Reference to the player movement script
    PlayerControls controls;
    /// Is the player currently sprinting?
    bool isSprinting = false;
    /// Current stamina value
    float stamina;

    void Start() {
        controls = GetComponent<PlayerControls>();
        stamina = maxStamina;
    }

    void Update() {
        Animator animator = GetComponentInChildren<Animator>();
        if(isSprinting) { // sprinting -> use up stamina, until depleted
            stamina -= Time.deltaTime * staminaConsumptionPerSec;
            if(stamina <= 0f) {
                stamina = 0f;
                Deactivate();
            }
            animator.SetBool("sprinting", true);
        } else { // not sprinting -> recover stamina
            stamina = Mathf.Min(maxStamina, stamina + Time.deltaTime * staminaRecoveryPerSec);
            
            animator.SetBool("sprinting", false);
        }

        // check whether sprinting was toggled and apply/remove speedup
		if(Input.GetButtonDown(Inputs.SprintToggle))
            Activate();
		if(Input.GetButtonUp(Inputs.SprintToggle))
            Deactivate();
    }

    //TODO might be interesting to modify the camera's FOV, to create an acceleration effect

    void Activate() {
        if(!isSprinting) {
            isSprinting = true;
            controls.movementSpeed *= speedup;
            controls.wooshVolumeFactor *= speedup;
        }
    }

    void Deactivate() {
        if(isSprinting) {
            isSprinting = false;
            controls.movementSpeed /= speedup;
            controls.wooshVolumeFactor /= speedup;
        }
    }

    float IExpressibleAsFraction.GetFraction() {
        return stamina / maxStamina;
    }

}

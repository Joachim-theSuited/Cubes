using UnityEngine;

/// <summary>
/// Player jump behaviour.
/// Allows controlled player to jump to a certain height based on global gravity; the higher the gravity, the higher you can jump
/// </summary>
[RequireComponent(typeof(PlayerControls))]
public class PlayerJumpBehaviour : MonoBehaviour {

    private Rigidbody _rigidbody;
    private ParticleSystem _jumpParticles;
    private bool canJump = true;
    private float chargeTime = 0f;

    public float jumpForce = 200f;
    public int jumpParticleCount = 50;

    // Use this for initialization
    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _jumpParticles = GetComponent<ParticleSystem>();
    }
	
    // Update is called once per frame
    void Update() {
        if(canJump && Input.GetButton(Inputs.Jump)) {
            chargeTime += Time.deltaTime;
        }

        if(canJump && Input.GetButtonUp(Inputs.Jump)) {
            _jumpParticles.Emit(jumpParticleCount);
            _rigidbody.AddForce(Vector3.up * jumpForce * (1 + Mathf.Min(chargeTime, 2f)));
            canJump = false;
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetTrigger("jump");
            chargeTime = 0;
        }
    }

    void OnCollisionEnter(Collision collision) {
        // when landing on a floor reenable jumping
        if(1 << collision.gameObject.layer == LayerMask.GetMask("Floors")) {
            canJump = true;
            _jumpParticles.Emit(jumpParticleCount);
        }
    }
}

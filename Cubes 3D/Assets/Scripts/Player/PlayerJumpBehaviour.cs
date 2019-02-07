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

    public readonly string[] jumpResetLayers = {CubesLayers.Floors, CubesLayers.Water};

    public AudioSource jumpSound;
    public AudioSource impactSound;

    private float dropStart;
    private float lastFrameAltitudeChange;

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
            jumpSound.Play();
            _rigidbody.AddForce(Vector3.up * jumpForce * (1 + Mathf.Min(chargeTime, 2f)));
            canJump = false;
            Animator animator = GetComponentInChildren<Animator>();
            animator.SetTrigger("jump");
            chargeTime = 0;
        }

        if(lastFrameAltitudeChange > 0 && _rigidbody.velocity.y < 0) {
            dropStart = _rigidbody.position.y;
        }
        lastFrameAltitudeChange = _rigidbody.velocity.y;
    }

    void OnCollisionEnter(Collision collision) {
        // when landing on a floor reenable jumping
        int layerMask = 1 << collision.gameObject.layer;
        if((layerMask & LayerMask.GetMask(jumpResetLayers)) == layerMask && dropStart - _rigidbody.position.y > 0.5f) {
            canJump = true;
            // can be moved into animation once we have a 'falling' animation state
            _jumpParticles.Emit(50);
        }

        // play impact sound only when dropped a certain height
        if(dropStart - _rigidbody.position.y > 0.5f) {
            impactSound.Play();
            dropStart = -1000;
        }
    }
}

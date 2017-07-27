using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Describes what an entity should do, when it tries to chase another entity.
/// This implementation will use Unity's NavMeshAgent to follow the target object.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Chase : MonoBehaviour {

    /// <summary>
    /// The time in seconds between recalculating the chase path.
    /// </summary>
    public static readonly float REPATHINTERVAL = 1f;

    /// <summary>
    /// The object to be chased.
    /// </summary>
    public Rigidbody target;

    /// <summary>
    /// The movement speed used while chasing in world units per second.
    /// </summary>
    public float movementSpeed = 300;
    private Vector3 currentTarget;

    private float timeLastRepath;

    private NavMeshAgent _navMeshAgent;

    // Use this for initialization
    void Start() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        repath();
    }
	
    // Update is called once per frame
    void Update() {
        timeLastRepath += Time.deltaTime;
        if(timeLastRepath > REPATHINTERVAL) {
            repath();
        }
    }

    /// <summary>
    /// Enables this MonoBehaviour but also does some housekeeping like recalculating pathing.
    /// Please use this method instead of setting enabled to true by hand.
    /// </summary>
    public void enable() {
        repath();
        _navMeshAgent.isStopped = false;
        enabled = true;
    }

    public void disable() {
        _navMeshAgent.isStopped = true;
        enabled = false;
    }

    /// <summary>
    /// Run a new A* search for the path to the target and reset the timer.
    /// </summary>
    private void repath() {
        _navMeshAgent.destination = target.position;
        timeLastRepath = 0;
    }
}

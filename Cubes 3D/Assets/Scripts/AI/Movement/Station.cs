using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// The Station Behaviour allows an entity to path to a given station.
/// Upon arrival the entity will sit there and do nothing else unless this Behaviour is used with a Demeanor.
/// If no path to the station could be found, the unit will simply stand still and do nothing.
/// Repathing could be triggered by calling enable().
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class Station : MonoBehaviour {

    public Vector3 station;

    public float movementSpeed = 300;

    private NavMeshAgent _navMeshAgent;

    // Use this for initialization
    void Start() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        repath();
    }
	
    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Enabled this MonoBehaviour but also does some housekeeping like recalculating pathing.
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
        _navMeshAgent.destination = station;
    }
}

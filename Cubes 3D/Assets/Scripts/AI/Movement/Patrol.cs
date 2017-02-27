using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// A moving entity can use the Patrol MonoBehaviour and a GameObject
/// containing several other GameObject (denoting the patrol points) to patrol.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Patrol : MonoBehaviour {

    public GameObject patrolWaypointsParent;

    public int currentWaypointIndex = -1;

    private static readonly float waypointEpsilon = 0.1f;

    public float movementSpeed = 300;

    private List<Transform> patrolWaypoints;
    private Vector3 currentTarget;
    private NavMeshAgent _navMeshAgent;

	// Use this for initialization
	void Start () {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        patrolWaypoints = new List<Transform>(patrolWaypointsParent.GetComponentsInChildren<Transform>());
        patrolWaypoints.Remove(patrolWaypointsParent.transform);
		if(currentWaypointIndex == -1)
        {
            currentWaypointIndex = 0;
        }
	}
	
    void Update()
    {
        if(_navMeshAgent.remainingDistance < waypointEpsilon)
        {
            // set next waypoint
            incrementWaypointIndex();
            repath();
        }
    }

    public void enable()
    {
        repath();
        _navMeshAgent.Resume();
        enabled = true;
    }

    public void disable()
    {
        _navMeshAgent.Stop();
        enabled = false;
    }

    private void repath()
    {
        _navMeshAgent.destination = patrolWaypoints[currentWaypointIndex].position;
    }

    /// <summary>
    /// Pick next waypoint or the first one if end of the list is reached.
    /// </summary>
    private void incrementWaypointIndex()
    {
        if(currentWaypointIndex+1 < patrolWaypoints.Count)
        {
            currentWaypointIndex++;
        }
        else
        {
            currentWaypointIndex = 0;
        }
    }
}

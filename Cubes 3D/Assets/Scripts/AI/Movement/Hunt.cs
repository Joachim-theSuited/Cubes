using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Hunt : MonoBehaviour {

    public static readonly float REPATHINTERVAL = 1f;

    public float backingUpSpeed = 10;

    public Rigidbody target;

    public float outerBoundary;
    public float innerBoundary;

    private float timeLastRepath;

    private Rigidbody _rigidbody;
    private NavMeshAgent _navMeshAgent;

    public void enable()
    {
        repath();
		getNavMeshAgent().Resume();
        enabled = true;
    }

    public void disable()
    {
		getNavMeshAgent().Stop();
        enabled = false;
    }

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        repath();
    }

    private void repath()
    {
        GetComponent<NavMeshAgent>().destination = target.position;
        timeLastRepath = 0;
    }
	
	// Update is called once per frame
	void Update () {
        timeLastRepath += Time.deltaTime;
        if(timeLastRepath > REPATHINTERVAL)
        {
            repath();
        }

        if((target.position-_rigidbody.position).magnitude < innerBoundary)
        {
            // back away from target
			getNavMeshAgent().Stop();
            backAway();
        }
        else
        {
			getNavMeshAgent().Resume();
        }
	}
    
    private void backAway()
    {
        Vector3 moveVector = (_rigidbody.position - target.position).normalized * Time.deltaTime * backingUpSpeed;
        _rigidbody.MovePosition(_rigidbody.position + moveVector);
    }

	private NavMeshAgent getNavMeshAgent() {
		if(_navMeshAgent == null) {
			_navMeshAgent = GetComponent<NavMeshAgent>();
		}
		return _navMeshAgent;
	}
}

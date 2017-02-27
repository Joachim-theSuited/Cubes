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
        _navMeshAgent.Resume();
        enabled = true;
    }

    public void disable()
    {
        _navMeshAgent.Stop();
        enabled = false;
    }

	// Use this for initialization
	void Start () {
        _navMeshAgent = GetComponent<NavMeshAgent>();
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
            _navMeshAgent.Stop();
            backAway();
        }
        else
        {
            _navMeshAgent.Resume();
        }
	}
    
    private void backAway()
    {
        Vector3 moveVector = (_rigidbody.position - target.position).normalized * Time.deltaTime * backingUpSpeed;
        _rigidbody.MovePosition(_rigidbody.position + moveVector);
    }
}

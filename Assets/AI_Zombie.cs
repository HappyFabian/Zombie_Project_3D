using UnityEngine;
using UnityEngine.Networking;

public class AI_Zombie : NetworkBehaviour {

	bool isIdling;
	NavMeshAgent agent;
	Transform target = null;
	float oldDistance = 999;
	SphereCollider colliderA;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		colliderA = GetComponent <SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (target != null) 
		{
			agent.SetDestination(target.position);
			oldDistance = Vector3.Distance(this.transform.position, target.transform.position);
			this.transform.LookAt(target);
		}
	}

	void OnTriggerStay(Collider col)
	{
		if (col.transform.tag == "Player") 
		{
			if(Vector3.Distance(this.transform.position,col.transform.position) < oldDistance)
			{
				target = col.transform;
			}
		}

	}
}

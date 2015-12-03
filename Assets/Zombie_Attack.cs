using UnityEngine;
using System.Collections;

using UnityEngine.Networking;

public class Zombie_Attack : NetworkBehaviour {

	private const string playerTag = "Player";
	
	public P_Weapon weapon;
	private LayerMask mask;

	public float attackTime = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//[Client]
	void Claw(string col)
	{
		Cmd_PlayerShot (col, weapon.Damage);
	}

	//[Command]
	void Cmd_PlayerShot(string ID, int damage)
	{
		Debug.Log (ID + " was clawed. It dealt " + damage + " damage");
		GameObject go = GameObject.Find (ID);
		go.GetComponent<CombatSystem> ().DeductHealth (damage);
		
	}

	void OnTriggerStay(Collider cold)
	{
		if (cold.transform.tag == playerTag) 
		{
			attackTime += Time.deltaTime;
			if(attackTime >=1)
			{
				Claw(cold.name);
				attackTime = 0;
			}
		}
	}
}

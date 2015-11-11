using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class DeathMechanic : NetworkBehaviour 
{
	private CombatSystem cSystem;
	private Text crosshair;


	// Use this for initialization
	void Start () 
	{
		crosshair = GameObject.Find ("CrossHair").GetComponent<Text> ();
		cSystem = GetComponent<CombatSystem> ();
		cSystem.EventDie += DisablePlayer;
	}

	void OnDisable()
	{
		cSystem.EventDie -= DisablePlayer;
	}


	void DisablePlayer()
	{
		GetComponent<P_Motor> ().enabled = false;
		GetComponent<P_Movement> ().enabled = false;
		GetComponent<P_GunMechanic> ().enabled = false;
		GetComponent<P_Weapon> ().enabled = false;
		GetComponent<CapsuleCollider> ().enabled = false;
		cSystem.isDead = false;

		Cursor.lockState = CursorLockMode.None;

		Renderer[] renders = GetComponentsInChildren<Renderer> ();
		foreach (Renderer ren in renders) 
		{
			ren.enabled = false;
		}
	}
}

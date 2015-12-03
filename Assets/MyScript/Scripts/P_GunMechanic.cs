using UnityEngine;
using UnityEngine.Networking;

public class P_GunMechanic : NetworkBehaviour 
{
	private const string playerTag = "Player";

	public P_Weapon weapon;
	public Material what;

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private LayerMask mask;

	// Use this for initialization
	void Start () 
	{
		if (cam == null) 
		{
			Debug.LogError("PlayerShoot: No camera Referenced");
			this.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Fire1"))
		{
			Shoot();
		}
		if (Input.GetButtonDown ("E")) 
		{
			GrabBullets();
		}
	}

	[Client]
	void Shoot()
	{
		if (weapon.ReadyAmmunition > 0) 
		{
			if (weapon.WeaponMechanic == 1)
			{
				RaycastHit hit;
				Ray ray = new Ray (cam.transform.position, cam.transform.forward);
				if (Physics.Raycast (ray, out hit, weapon.Range, mask)) 
				{
					if (hit.collider.tag == playerTag)
					{
						Cmd_PlayerShot (hit.collider.name, weapon.Damage);
					}
					if(hit.collider.tag == "Zombie")
					{
						Cmd_PlayerShot(hit.collider.name,weapon.Damage);
					}
				}
				weapon.DeductRdyBullets(1);
			}
		} 
		else 
		{
			weapon.Reload();
		}

	
	}

	[Client]
	void GrabBullets()
	{
		RaycastHit hit;
		Ray ray = new Ray (cam.transform.position, cam.transform.forward);
		if (Physics.Raycast (ray, out hit, weapon.Range, mask)) 
		{
			if (hit.collider.tag == "ammoCache")
			{
				weapon.ObtainMoreBullets(30);
				Cmd_PlayerGotCache(hit.collider.name);
			}
		}
	}

	[Command]
	void Cmd_PlayerGotCache(string ID)
	{
		GameObject go = GameObject.Find (ID);
		go.GetComponent<CombatSystem> ().DeductHealth (1);

	}


	[Command]
	void Cmd_PlayerShot(string ID, int damage)
	{
		Debug.Log (ID + " was Shot. It dealt " + damage + " damage");
		GameObject go = GameObject.Find (ID);
		go.GetComponent<CombatSystem> ().DeductHealth (damage);

	}


}

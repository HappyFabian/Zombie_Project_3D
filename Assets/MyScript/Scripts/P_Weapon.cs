using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class P_Weapon : NetworkBehaviour
{
	public string WeaponName = "Glock";
	public string WeaponKind = "Handgun";
	// 1 = Bullets, 2 = Projectiles, 3 = Grenade, 4 = Melee
	public int WeaponMechanic = 1;

	public int Damage = 10;
	public float Range = 100f;


	public int GunCapacity = 7;
	[SyncVar(hook = "OnStkAmm")] public int StockAmmunition = 35;
	[SyncVar(hook = "OnRdyAmm")] public int ReadyAmmunition = 7;


	private Text AmmoTxt;
	private Text StkAmmoTxt;

	void Start()
	{
		AmmoTxt = GameObject.Find("AmmoText").GetComponent<Text>();
		StkAmmoTxt = GameObject.Find("StockAmmoText").GetComponent<Text>();
		SetStkBullets ();
		SetRdyBullets ();
	}

	void OnStkAmm(int Stk)
	{
		StockAmmunition = Stk;
	}

	void OnRdyAmm(int Rdy)
	{
		ReadyAmmunition = Rdy;
	}

	public void DeductRdyBullets(int bullet)
	{
		ReadyAmmunition -= bullet;
		SetRdyBullets ();
	}
	public void DeductStkBullets(int bullet)
	{
		StockAmmunition -= bullet;
		SetStkBullets ();
	}

	void SetStkBullets()
	{
		if (isLocalPlayer) 
		{
			StkAmmoTxt.text = "Stock: " + StockAmmunition.ToString ();
		}
	}
	void SetRdyBullets()
	{
		if (isLocalPlayer) 
		{
			AmmoTxt.text = "Ammo: " + ReadyAmmunition.ToString ();
		}
	}

	public void ObtainMoreBullets(int bullets)
	{
		StockAmmunition += bullets;
		SetStkBullets ();
	}


	public void Reload()
	{
		if (ReadyAmmunition < GunCapacity) 
		{
			for(int i = ReadyAmmunition; (i <GunCapacity)&&(StockAmmunition > 0);i++)
			{
				DeductRdyBullets(-1);
				DeductStkBullets(1);
			}
		}
	}
}

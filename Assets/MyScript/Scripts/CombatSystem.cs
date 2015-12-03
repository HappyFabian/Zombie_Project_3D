using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class CombatSystem : NetworkBehaviour 
{
	[SyncVar(hook = "OnHealthChanged")] public int health = 100;
	private Text healthText;
	[SerializeField]
	private bool shouldDie = false;	
	public bool isDead = false;

	public delegate void DieDelegate();
	public event DieDelegate EventDie;

	void CheckCondition()
	{
		if (health <= 0 && shouldDie) 
		{
			if(EventDie != null)
			{
				EventDie();
			}
			shouldDie = false;
			isDead = true;
		}
	}


	void Start()
	{
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		SetHealthText ();
	}

	// Update is called once per frame
	void Update () 
	{
		CheckCondition ();
	}

	void SetHealthText()
	{
		if (isLocalPlayer) 
		{
			healthText.text = "Health: " + health.ToString ();
		}
	}

	public void DeductHealth(int damage)
	{
		if (!isDead) 
		{
			health -= damage;
		}
	}

	void OnHealthChanged(int hlth)
	{
		health = hlth;
		SetHealthText ();
	}




}

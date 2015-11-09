using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class CombatSystem : NetworkBehaviour 
{
	[SyncVar(hook = "OnHealthChanged")] public int health = 100;
	private Text healthText;

	void Start()
	{
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		SetHealthText ();
	}

	// Update is called once per frame
	void Update () 
	{

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
		health -= damage;
	}

	void OnHealthChanged(int hlth)
	{
		health = hlth;
		SetHealthText ();
	}




}

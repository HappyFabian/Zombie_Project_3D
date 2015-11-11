using UnityEngine;
using UnityEngine.Networking;

public class P_Setup : NetworkBehaviour 
{
	[SerializeField]
	Behaviour[] componentsToDisable;

	[SerializeField]
	string remotelayername = "RemotePlayer";

	public Material what;

	Camera sceneCamera;


	void Start()
	{
		RegisterPlayer ();

		if (!isLocalPlayer) 
		{
			DisableComponents();
			AssignRemoteLayer();
		} 
		else 
		{
			this.GetComponentInChildren<MeshRenderer>().material = what;
			DisableSceneCamera();
			
			Cursor.lockState = CursorLockMode.Locked;
		}

	}
	void RegisterPlayer()
	{
		string ID = "Player_" + GetComponent<NetworkIdentity> ().netId;
		transform.name = ID;
	}

	void AssignRemoteLayer()
	{
		gameObject.layer = LayerMask.NameToLayer (remotelayername);
	}

	void DisableComponents()
	{
		for (int i = 0; i< componentsToDisable.Length; i++) 
		{
			componentsToDisable [i].enabled = false;
		}
	}
	void DisableSceneCamera()
	{
		sceneCamera = Camera.main;
		if(sceneCamera != null)
		{
			sceneCamera.gameObject.SetActive(false);
		}
	}
	
	void OnDisable()
	{
		if (sceneCamera != null) 
		{
			sceneCamera.gameObject.SetActive (true);
		}
	}
}

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Character_Controller : NetworkBehaviour 
{
	public float speed; 
	public float rotate_speed;//Amount of speed the character has while moving.

	public float gravity;
	public Material what;


	// Use this for initialization
	void Start () 
	{
		if (isLocalPlayer) {
		
			GetComponent<MeshRenderer>().material = what;


			Camera.main.transform.parent = this.transform.FindChild("CameraHolder").transform;
			Camera.main.transform.position = this.transform.FindChild("CameraHolder").position;
			Camera.main.transform.rotation = this.transform.FindChild("CameraHolder").rotation;
		}
	}
		
	// Update is called once per frame
	void Update () 
	{	
	
	
		if (!isLocalPlayer) 
		{
			return;
		}


		this.transform.Rotate(0, rotate_speed * Time.deltaTime * Input.GetAxis("Mouse X"),0);

		Vector3 movement = speed * Time.deltaTime * Input.GetAxis ("Vertical") *this.transform.forward;
		movement += speed * Time.deltaTime * Input.GetAxis ("Horizontal") * this.transform.right;
		movement.y -= gravity * Time.deltaTime;

		GetComponent<CharacterController> ().Move (movement);

	}

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(P_Motor))]
public class P_Movement : MonoBehaviour {

	[SerializeField] 
	public float speed = 10f;
	[SerializeField]
	public float mouse_sensitivity = 5f;

	private P_Motor playerMotor;
	
	// Use this for initialization
	void Start () 
	{
		playerMotor = GetComponent<P_Motor> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Movement vectors gotten from Axis.
		float _xM = Input.GetAxisRaw ("Horizontal");
		float _zM = Input.GetAxisRaw ("Vertical");

		float _yRot = Input.GetAxisRaw ("Mouse X");
		float _xRot = Input.GetAxisRaw ("Mouse Y");

		Vector3 _mHorizontal = this.transform.right * _xM;
		Vector3 _mVertical = this.transform.forward * _zM;


		//Normalized velocity Vector
		Vector3 _velocity = (_mHorizontal + _mVertical).normalized * speed;
		//Player Rotation velocity;
		Vector3 _rotation = new Vector3 (0, _yRot, 0) * mouse_sensitivity;
		//Camera Rotation Up and Down
		float _cameraRotation =_xRot * mouse_sensitivity;

		playerMotor.AssignVelocity (_velocity);
		playerMotor.AssignRotationVelocity (_rotation);
		playerMotor.AssignCameraRotationVelocity (_cameraRotation);
	}
}

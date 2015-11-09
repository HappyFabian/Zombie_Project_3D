using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class P_Motor : MonoBehaviour 
{
	[SerializeField]
	public Camera cam;

	Vector3 velocity = Vector3.zero;
	Vector3 rotationVelocity = Vector3.zero;
	float camerarotationVelocity = 0f;
	float currentCameraRotationX = 0f;

	[SerializeField]
	private float cameraRotationLimit = 85f;


	Rigidbody rigidBody;


	// Use this for initialization
	void Start () 
	{
		rigidBody = GetComponent<Rigidbody> ();
	}
	public	void AssignVelocity(Vector3 temp_velocity)
	{
		velocity = temp_velocity;
	}

	public void AssignRotationVelocity(Vector3 temp_rotation)
	{
		rotationVelocity = temp_rotation;
	}
	public void AssignCameraRotationVelocity(float temp_camera_rotation)
	{
		camerarotationVelocity = temp_camera_rotation;
	}

	void Rotate()
	{
		rigidBody.MoveRotation (rigidBody.rotation*Quaternion.Euler(rotationVelocity));
	}

	void CameraRotate()
	{
		if ((cam != null))
		{
			currentCameraRotationX -= camerarotationVelocity;
			currentCameraRotationX = Mathf.Clamp(currentCameraRotationX,-cameraRotationLimit,cameraRotationLimit);
			cam.transform.localEulerAngles = new Vector3(currentCameraRotationX,0,0);
		}

	}

	void Move ()
	{

		if (velocity != Vector3.zero) 
		{
			rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
		}
	}

	void FixedUpdate ()
	{
		Move ();
		Rotate ();
		CameraRotate ();
	}
}	

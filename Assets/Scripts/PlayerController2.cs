using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
	private Rigidbody rb = null;

	[SerializeField] private float swimSpeed = 1f;
	[SerializeField] private float maxAcceleration = 5f;
	[SerializeField] private float turnSpeed = 1f;
	
	private Vector3 velocity, movementVector;

	private float pitch, yaw = 0f;

	void Awake() 
	{
		rb = GetComponent<Rigidbody>();
	}

	void Start()
	{
		
	}

	void Update()
	{
		Vector2 input;
		input.x = Input.GetAxis("Horizontal");
		input.y = Input.GetAxis("Vertical");
		input = Vector2.ClampMagnitude(input, 1f);

		pitch = Input.GetAxis("Mouse Y");
		pitch = Mathf.Clamp(pitch, -1f, 1f);
		pitch *= turnSpeed;
		yaw = Input.GetAxis("Mouse X");
		yaw = Mathf.Clamp(yaw, -1f, 1f);
		yaw *= turnSpeed;

		this.transform.Rotate(pitch * Time.deltaTime, 0f, yaw * Time.deltaTime);

		if(input.y > 0)
			movementVector = new Vector3(input.x, 0f, -input.y) * swimSpeed;
		else
			movementVector = new Vector3(input.x, 0f, 0f) * swimSpeed;

		float maxSpeedChange = maxAcceleration * Time.deltaTime;
		velocity.z = Mathf.MoveTowards(velocity.z, movementVector.z, maxSpeedChange);

		rb.velocity += -transform.up * velocity.z * swimSpeed;
	}
}

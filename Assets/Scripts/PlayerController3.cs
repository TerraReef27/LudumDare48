using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3 : MonoBehaviour
{
	private Rigidbody rb = null;

	[SerializeField] private float swimSpeed = 1f;
	[SerializeField] private float maxAcceleration = 5f;
	[SerializeField] private float turnSpeed = 1f;
	
	private Vector3 velocity, movementVector;

	private float pitch, yaw = 0f;

	[SerializeField] private Transform cam = null;

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

		pitch = -input.y*turnSpeed*Time.deltaTime;
		yaw = -input.x*turnSpeed*Time.deltaTime;
		
		transform.Rotate(pitch, 0f, yaw);

		if(Input.GetButton("Jump"))
		{
			Vector3 moveTo = this.transform.position + (this.transform.up * swimSpeed * Time.deltaTime);
			rb.MovePosition(moveTo);
		}			
	}
}

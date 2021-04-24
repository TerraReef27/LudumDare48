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
	private Animator animator = null;

	void Awake() 
	{
		rb = GetComponent<Rigidbody>();
		animator = GetComponentInChildren<Animator>();
	}

	void Start()
	{
		
	}

	void Update()
	{
		animator.SetBool("Movement", false);

		Vector2 input;
		input.x = Input.GetAxis("Horizontal");
		input.y = Input.GetAxis("Vertical");
		input = Vector2.ClampMagnitude(input, 1f);

		if(Mathf.Abs(input.x) > 0 || Mathf.Abs(input.y) > 0)
		{
			animator.SetBool("Movement", true);
		}

		pitch = -input.y*turnSpeed*Time.deltaTime;
		yaw = -input.x*turnSpeed*Time.deltaTime;
		
		transform.Rotate(pitch, 0f, yaw);

		Debug.Log(this.transform.up);
		if(Input.GetButton("Jump"))
		{
			if(this.transform.position.y < 0f || this.transform.up.y <= 0)
			{
				animator.SetBool("Movement", true);
				Vector3 move = this.transform.up * swimSpeed * Time.deltaTime;
				Debug.Log(move);
				Vector3 moveTo = this.transform.position + (move);
				rb.MovePosition(moveTo);
			}
		}
	}
}

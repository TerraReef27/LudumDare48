using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
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

		if(input.y > 0)
			movementVector = new Vector3(input.x, 0f, -input.y) * swimSpeed;
		else
			movementVector = new Vector3(input.x, 0f, 0f) * swimSpeed;

		if(input.x != 0)
		{
			Vector3 rotation = new Vector3(0f, 0f, (this.transform.rotation.z + input.x) * turnSpeed * Time.deltaTime);
			this.transform.Rotate(rotation);
		}
		
		
		if(!(this.transform.localRotation.eulerAngles.x <= 0 && pitch > 0f))
		this.transform.Rotate(pitch * Time.deltaTime, 0f, 0f);
    }
	
	void FixedUpdate()
	{
		float maxSpeedChange = maxAcceleration * Time.deltaTime;
		
		velocity.z = Mathf.MoveTowards(velocity.z, movementVector.z, maxSpeedChange);

		rb.position += velocity;
	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private CharacterController controller = null;

	[SerializeField] private float swimSpeed = 1f;
	[SerializeField] private float maxAcceleration = 5f;
	private Vector3 velocity, movementVector;

	void Awake() 
	{
		controller = GetComponent<CharacterController>();
	}

    void Start()
    {
        
    }

    void Update()
    {
		movementVector = Vector3.zero;
        Vector2 input;
		input.x = Input.GetAxis("Horizontal");
		input.y = Input.GetAxis("Vertical");
		input = Vector2.ClampMagnitude(input, 1f);

		if(input.y > 0)
			movementVector = -Vector3.forward * input.y * swimSpeed;
		
		controller.Move(movementVector * Time.deltaTime * swimSpeed);
    }
}

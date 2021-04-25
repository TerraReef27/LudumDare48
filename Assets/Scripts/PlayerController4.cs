using UnityEngine;

public class PlayerController4 : MonoBehaviour
{
	[SerializeField] private Vector3 startPosition;
	private Rigidbody rb = null;

	[SerializeField] private float maxSwimHeight = -.2f;
	[SerializeField] private float swimSpeed = 1f;
	public float SwimSpeed {get{return swimSpeed;} set{swimSpeed=value;}}
	[SerializeField] private float swimDeceleration = 2f;
	
	[SerializeField] private float turnSpeed = 1f;

	private float pitch, yaw = 0f;
	private Vector3 move, movementVector;
	[SerializeField] private Vector3 velocity;
	public Vector3 Velocity {get {return velocity;}}
	Vector2 input;
	private bool isSwimming;
	public Vector3 Move {get {return move;}}

	private Animator animator = null;
	private GameManager gameManager;

	void Awake() 
	{
		rb = GetComponent<Rigidbody>();
		animator = GetComponentInChildren<Animator>();
		gameManager = FindObjectOfType<GameManager>();
		gameManager.OnReset += GameManager_OnReset;
	}

	void Update()
	{
		animator.SetBool("Movement", false);

		if(!gameManager.paused)
		{
			input.x = Input.GetAxis("Horizontal");
			input.y = Input.GetAxis("Vertical");
			input = Vector2.ClampMagnitude(input, 1f);

			if(Mathf.Abs(input.x) > 0 || Mathf.Abs(input.y) > 0)
			{
				animator.SetBool("Movement", true);
			}

			if(Input.GetButton("Jump"))
			{
				isSwimming = true;
				animator.SetBool("Movement", true);
			}
			else
			{
				isSwimming = false;
			}
		}
	}

	void FixedUpdate()
	{
		if(!gameManager.paused)
		{
			pitch = -input.y*turnSpeed*Time.deltaTime;
			yaw = -input.x*turnSpeed*Time.deltaTime;
			
			transform.Rotate(pitch, 0f, yaw);

			if(isSwimming)
			{
				velocity = this.transform.up * swimSpeed;
			}
			else
			{
				velocity = Vector3.MoveTowards(velocity, Vector3.zero, swimDeceleration * Time.deltaTime);
			}

			if(this.transform.position.y >= maxSwimHeight && velocity.y > 0)
			{
				velocity.y = 0f;
			}

			Vector3 displacement = velocity * Time.deltaTime;

			rb.MovePosition(this.transform.localPosition + displacement);
		}
	}

	private void GameManager_OnReset()
	{
		this.gameObject.transform.position = startPosition;
		velocity = Vector3.zero;
	}
}

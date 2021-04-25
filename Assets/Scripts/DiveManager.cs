using UnityEngine;

public class DiveManager : MonoBehaviour
{
	[SerializeField] private float maxPsi;
	public float MaxPsi {get{return maxPsi;} set{maxPsi=value;}}
    [SerializeField] private float psi;
	public float Psi {get{return psi;}}
	private float depth;
	public float Depth {get{return depth;}}
	private float maxDepth;
	[SerializeField] private float airEfficiency;
	public float AirEfficiency {get{return airEfficiency;} set{airEfficiency=value;}}
	private float airLossRate;
	[SerializeField] private float maxAscentValue;
	public float MaxAscentValue {get{return maxAscentValue;}}
	private float ascentRate;
	public float AscentRate {get{return ascentRate;}}
	private float ascentValue;
	public float AscentValue {get{return ascentValue;}}
	[SerializeField] private float ascentReduction;
	public float AscentReduction {get{return ascentReduction;} set{ascentReduction=value;}}

	private PlayerController4 playerController;
	private GameManager gameManager;

	void Awake()
	{
		playerController = GetComponent<PlayerController4>();
		gameManager = FindObjectOfType<GameManager>();
		gameManager.OnReset += GameManager_OnReset;
	}

    void Start()
    {
		psi = maxPsi;

		CalculateAirLoss();
    }

    
    void Update()
    {
		if(!gameManager.paused)
		{
			psi += airLossRate * Time.deltaTime;;
			psi = Mathf.Clamp(psi, 0f, maxPsi);

			if(psi <= 0)
			{
				gameManager.TriggerBlackout();
			}

			depth = this.transform.position.y;

			if(depth > maxDepth)
				maxDepth = depth;

			CalculateAirLoss();
			CalculateAscentRate();
		}
    }

	private void CalculateAirLoss()
	{
		airLossRate = depth / airEfficiency;
	}

	private void CalculateAscentRate()
	{
		ascentRate = playerController.Velocity.y * 10f * Time.deltaTime;
		ascentValue += Mathf.Clamp(ascentRate, 0f, maxAscentValue);
		ascentValue -= Mathf.Clamp(ascentValue * (ascentReduction * Time.deltaTime), 0f, maxAscentValue);

		if(ascentValue >= maxAscentValue)
		{
			gameManager.TriggerBlackout();
		}
	}

	private void GameManager_OnReset()
	{
		psi = maxPsi;
		ascentRate = 0f;
		ascentValue = 0f;
	}
}

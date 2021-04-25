using UnityEngine;

public class DiveManager : MonoBehaviour
{
	[SerializeField] private float maxPsi;
	public float MaxPsi {get{return maxPsi;}}
    [SerializeField] private float psi;
	public float Psi { get { return psi; } }
	private float depth;
	public float Depth {get{return depth;}}
	private float maxDepth;
	[SerializeField] private float airEfficiency;
	private float airLossRate;
	[SerializeField] private float maxAscentValue;
	public float MaxAscentValue {get{return maxAscentValue;}}
	private float ascentRate;
	public float AscentRate {get{return ascentRate;}}
	private float ascentValue;
	public float AscentValue {get{return ascentValue;}}
	[SerializeField] private float ascentReduction;
	private PlayerController4 playerController;

    void Start()
    {
		psi = maxPsi;
        playerController = GetComponent<PlayerController4>();

		CalculateAirLoss();
    }

    
    void Update()
    {
		psi += airLossRate * Time.deltaTime;;
        psi = Mathf.Clamp(psi, 0f, maxPsi);
		depth = this.transform.position.y;

		if(depth > maxDepth)
			maxDepth = depth;

		CalculateAirLoss();
		CalculateAscentRate();
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
	}
}

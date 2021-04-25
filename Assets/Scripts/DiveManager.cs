using System.Collections;
using System.Collections.Generic;
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
	private float maxAscentRate;
	public float MaxAscentRate {get{return maxAscentRate;}}
	private float ascentRate;
	public float AscentRate {get{return ascentRate;}}
	private Vector3 previousPos;
	private PlayerController3 PlayerController;

    void Start()
    {
		psi = maxPsi;
        PlayerController = GetComponent<PlayerController3>();

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
		if(!(depth > 10f))
		{
			Vector3 posDiff = this.transform.position - previousPos;
			if(posDiff.y > 0f)
			{
				ascentRate += posDiff.y;
			}
		}

		this.transform.position = previousPos;
	}
}

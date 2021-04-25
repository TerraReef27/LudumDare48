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
	private List<float> speedList;
	private int speedListMax = 10;
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
		float posDiff = this.transform.position.y - previousPos.y;

		if(speedList.Count < speedListMax)
		{
			speedList.Add(posDiff);
		}
		else if(speedList.Count >= speedListMax)
		{
			speedList.RemoveAt(0);
			speedList.Add(posDiff);
		}

		float averageAscentSpeed = 0f;
		if(speedList.Count > 0)
		{
			foreach(float speed in speedList)
			{
				averageAscentSpeed += speed;
			}
			averageAscentSpeed /= speedList.Count;
		}

		float metersPerMinute = averageAscentSpeed * 60; //Not actually represetnative of meters per second

		if(metersPerMinute > 0f)
		{
			ascentRate += averageAscentSpeed;
		}
		if(!(depth > 10f))
		{
			
		}

		this.transform.position = previousPos;
	}
}

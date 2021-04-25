using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionController : MonoBehaviour
{
	[SerializeField] private float maxCompacity;
	public float MaxCompacity {get{return maxCompacity;}}
	private float currentCompacity;
	public float CurrentCompacity {get{return currentCompacity;}}
	[SerializeField] private int currency;
	private List<Collectable> collectables;

	private bool clickPickup;
	private float pickupTimer = 0f;
	private float pickupTimerMax = .3f;

    private Collider characterCollider = null;

	void Awake()
	{
		characterCollider = GetComponent<Collider>();
		collectables = new List<Collectable>();
	}

    void Start()
    {
		currentCompacity = 0f;	
		pickupTimer = 0f;
    }

    
    void Update()
    {
		pickupTimer = Mathf.Clamp(pickupTimer - Time.deltaTime, 0f, pickupTimerMax);
		if(pickupTimer <= 0)
			clickPickup = false;
		else
			clickPickup = true;

		if(Input.GetButtonDown("Interact"))
		{
			pickupTimer = pickupTimerMax;
		}
    }

	void OnTriggerStay(Collider other)
	{		
		if(other.gameObject.tag == "Collectable")
		{
			if(clickPickup)
			{
				pickupTimer = 0;
				clickPickup = false;

				Collectable collectable = other.gameObject.GetComponent<Collectable>();
				if(collectable)
				{
					if(!(currentCompacity + collectable.weight > maxCompacity))
					{
						collectables.Add(collectable);
						currentCompacity += collectable.weight;
						currency += collectable.value;
						collectable.Collect();
					}
				}
			}
		}
	}
}

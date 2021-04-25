using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionController : MonoBehaviour
{
	[SerializeField] private float carryCompacity;
	private float currentCompacity;
	[SerializeField] private int currency;
	private List<Collectable> collectables;

	private bool clickPickup;

    private Collider characterCollider = null;
    void Start()
    {
        characterCollider = GetComponent<Collider>();
		currentCompacity = 0f;
		collectables = new List<Collectable>();
    }

    
    void Update()
    {
		clickPickup = Input.GetButtonDown("Interact");
    }

	void OnTriggerStay(Collider other)
	{
		Debug.Log("In Trigger");
		if(other.gameObject.tag == "Collectable")
		{
			Debug.Log("In Collectable");
			if(clickPickup)
			{
				Debug.Log("Interacted");
				Collectable collectable = other.gameObject.GetComponent<Collectable>();
				if(collectable)
				{
					if(!(currentCompacity + collectable.weight > carryCompacity))
					{
						collectables.Add(collectable);
						currentCompacity += collectable.weight;
						collectable.Collect();
					}
				}
			}
		}
	}
}

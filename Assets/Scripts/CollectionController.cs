using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionController : MonoBehaviour
{
	[SerializeField] private float maxCompacity;
	public float MaxCompacity {get{return maxCompacity;} set{maxCompacity = value;}}
	private float currentCompacity;
	public float CurrentCompacity {get{return currentCompacity;}}
	[SerializeField] private int currency;
	public int totalCurrency = 0;
	public float currencyMultiplier = 1f;

	private List<Collectable> collectables;

	private bool clickPickup;
	private float pickupTimer = 0f;
	private float pickupTimerMax = .3f;

    private Collider characterCollider = null;
	private GameManager gameManager;
	private UIManager uiManager;

	public event OnCashInDeligate OnCashIn;
    public delegate void OnCashInDeligate(int cratesCollected, int currencyEarned);

	void Awake()
	{
		uiManager = FindObjectOfType<UIManager>();
		characterCollider = GetComponent<Collider>();
		collectables = new List<Collectable>();
		gameManager = FindObjectOfType<GameManager>();
		gameManager.OnReset += GameManager_OnReset;
		gameManager.OnBlackout += GameManager_OnBlackout;
	}

    void Start()
    {
		totalCurrency = 0;
		currentCompacity = 0f;	
		pickupTimer = 0f;
    }

    
    void Update()
    {
		if(!gameManager.paused)
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
    }

	void OnTriggerStay(Collider other)
	{		
		if(other.gameObject.tag == "Collectable")
		{
			Collectable collectable = other.gameObject.GetComponent<Collectable>();
			if(!(currentCompacity + collectable.weight > maxCompacity))
			{
				uiManager.interactText.text = "E to pickup";
				if(clickPickup)
				{
					pickupTimer = 0;
					clickPickup = false;

					collectables.Add(collectable);
					currentCompacity += collectable.weight;
					currency += collectable.value;
					collectable.Collect();
				}
			}
			else
			{
				uiManager.interactText.text = "Not enough carry compacity";
			}
		}
		else if(other.gameObject.tag == "Shop")
		{
			if(clickPickup)
			{
				Debug.Log("called");
				pickupTimer = 0;
				clickPickup = false;
				CashIn();
			}
			uiManager.interactText.text = "E to deposit salvage";
		}
		else
		{
			uiManager.interactText.text = "";
		}
	}

	private void CashIn()
	{
		gameManager.SwitchUI(1);

		int currencyGained = 0;
		int cratesSalvaged = 0;
		foreach (Collectable crate in collectables)
		{
			currencyGained += crate.value;
			cratesSalvaged++;
		}
		collectables.Clear();
		totalCurrency+=currencyGained;
		currentCompacity = 0;

		OnCashIn?.Invoke(cratesSalvaged, currencyGained);
	}

	private void GameManager_OnReset()
	{
		
	}
	private void GameManager_OnBlackout()
	{
		totalCurrency = 0;
		currentCompacity = 0;
		collectables.Clear();
	}
}

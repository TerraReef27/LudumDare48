using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class Purchaseable : MonoBehaviour
{
	//[SerializeField] private int cost;
	[SerializeField] private int[] costs;
	//[SerializeField] private float purchasableValue;
	[SerializeField] private float[] values;
	private int currentIteration;

	[TextArea()][SerializeField] private string description;

	[SerializeField] private TMP_Text costText;
	[SerializeField] private TMP_Text buttonText;
	[SerializeField] private TMP_Text descriptionText;
	
	private Shop shop;
	private bool isDisabled;

	[SerializeField] private UnityEvent purchaseEvent;

	void OnEnable()
	{
		shop = FindObjectOfType<Shop>();
		UpdateUI();
		isDisabled = false;
	}

	public void OnClick()
	{
		if(currentIteration < costs.Length)
		{
			if(shop.Purchase(costs[currentIteration]))
			{
				purchaseEvent.Invoke();
				Debug.Log("Purchased!");
				currentIteration++;
				if(currentIteration >= costs.Length)
				{
					DisableButton();
					return;
				}
				UpdateUI();
			}
		}
		else
		{
			DisableButton();
		}
	}

	private void UpdateUI()
	{
		costText.text = "Cost: $"+costs[currentIteration];
		descriptionText.text = description + " " + values[currentIteration];
	}
	private void DisableButton()
	{
		buttonText.text = "No Furthur Upgrades";
		costText.text = "N/A";
		descriptionText.text = description + " N/A";
		GetComponent<Button>().interactable = false;
	}

	public void UpgradeAirCap()
	{
		shop.DiveManager.MaxPsi += values[currentIteration];
	}
	public void UpgradeAscentReduction()
	{
		shop.DiveManager.AscentReduction *= values[currentIteration];
	}
	public void UpgradeSwimSpeed()
	{
		shop.PlayerController.SwimSpeed += values[currentIteration];
	}
	public void UpgradeAirConsuption()
	{
		shop.DiveManager.AirEfficiency *= values[currentIteration];
	}
	public void UpgradeCarryCompacity()
	{
		shop.DiveManager.AirEfficiency *= values[currentIteration];
	}
	public void UpgradeCurrencyMultiplier()
	{
		shop.CollectionController.currencyMultiplier *= values[currentIteration];
	}
}
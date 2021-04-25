using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{	
	[SerializeField] private TMP_Text moneyText;

	private DiveManager diveManager;
	public DiveManager DiveManager{get{return diveManager;}}
	private PlayerController4 playerController;
	public PlayerController4 PlayerController{get{return playerController;}}
	private CollectionController collectionController;
	public CollectionController CollectionController{get{return collectionController;}}

	void OnEnable()
	{
		diveManager = FindObjectOfType<DiveManager>();
		collectionController = FindObjectOfType<CollectionController>();
		playerController = FindObjectOfType<PlayerController4>();
		UpdateUI();
	}
	public bool Purchase(int cost)
	{
		if(cost - collectionController.totalCurrency > 0)
		{
			collectionController.totalCurrency -= cost;
			UpdateUI();
			return true;
		}
		else
		{
			return false;
		}
	}

	private void UpdateUI()
	{
		moneyText.text = "$"+collectionController.totalCurrency;
	}
}

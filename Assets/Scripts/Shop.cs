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
	void OnEnable()
	{
		diveManager = FindObjectOfType<DiveManager>();
		UpdateUI();
	}
	public bool Purchase(int cost)
	{
		if(cost - diveManager.totalMoney > 0)
		{
			diveManager.totalMoney -= cost;
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
		moneyText.text = "$"+diveManager.totalMoney;
	}
}

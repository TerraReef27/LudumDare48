using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EarningsUI : MonoBehaviour
{
	private int crates;
	private int earnings;

    [SerializeField] private TMP_Text crateNumTxt;
	[SerializeField] private TMP_Text earnedTxt;
	[SerializeField] private TMP_Text totalTxt;

	private CollectionController collectionController;

	void Awake()
	{
		collectionController = FindObjectOfType<CollectionController>();
		collectionController.OnCashIn += CollectionController_OnCashIn;
	}

	public void UpdateUI()
	{
		crateNumTxt.text = crates.ToString();
		earnedTxt.text = "$"+earnings;
		totalTxt.text = "$"+collectionController.totalCurrency;
	}

	public void ShopBttn()
	{
		FindObjectOfType<GameManager>().SwitchUI(2);
	}

	private void CollectionController_OnCashIn(int crateNum, int earned)
	{
		crates = crateNum;
		earnings = earned;
		UpdateUI();
	}
}

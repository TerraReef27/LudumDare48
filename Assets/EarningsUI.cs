using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EarningsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text crateNumTxt;
	[SerializeField] private TMP_Text earnedTxt;
	[SerializeField] private TMP_Text totalTxt;

	private DiveManager diveManager;

    void OnEnable()
	{
		diveManager = FindObjectOfType<DiveManager>();
	}

	public void UpdateUI()
	{

	}

	public void ShopBttn()
	{

	}
}

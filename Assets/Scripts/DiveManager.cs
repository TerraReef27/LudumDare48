using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiveManager : MonoBehaviour
{
	[SerializeField] private float maxPsi;
    [SerializeField] private float psi;
	[SerializeField] private float depth;
	[SerializeField] private float airlossRate;
	[SerializeField] private float uiUpdateRate;
	private float currentUITime;
	private PlayerController3 PlayerController;
	[SerializeField] private Slider psiSlider;
	[SerializeField] private TMP_Text psiNumber;

    void Start()
    {
		currentUITime = 0;
		psi = maxPsi;
        PlayerController = GetComponent<PlayerController3>();
    }

    
    void Update()
    {
        psi -= airlossRate * Time.deltaTime;

		UpdateUI();
    }

	private void UpdateUI()
	{
		if(currentUITime >= uiUpdateRate)
		{
			int clampPsi = Mathf.FloorToInt(psi);
			psiNumber.text = "PSI: " + clampPsi;
			psiSlider.value = psi / maxPsi;
			currentUITime = 0f + (currentUITime % uiUpdateRate);
		}
		else
		{
			currentUITime += Time.deltaTime;
		}
	}
}

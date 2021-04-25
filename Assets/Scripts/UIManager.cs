using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	[SerializeField] private float uiUpdateRate;
	[SerializeField] private Slider psiSlider;
	[SerializeField] private TMP_Text psiNumber;
	[SerializeField] private TMP_Text depthNumber;
	[SerializeField] private Slider ascentSlider;
	[SerializeField] private TMP_Text ascentText;
	private float currentUITime;
	private DiveManager divemanager;

	void Awake()
	{
		divemanager = FindObjectOfType<DiveManager>();
	}
	void Start()
	{
		currentUITime = uiUpdateRate;
	}

	void Update()
	{
		if(currentUITime >= uiUpdateRate)
		{
			int clampPsi = Mathf.FloorToInt(divemanager.Psi);
			psiNumber.text = "PSI: " + clampPsi;
			psiSlider.value = divemanager.Psi / divemanager.MaxPsi;
			currentUITime = 0f + (currentUITime % uiUpdateRate);
		}
		else
		{
			currentUITime += Time.deltaTime;
		}
	
		int clampDepth = Mathf.FloorToInt(divemanager.Depth);
		clampDepth = Mathf.Abs(clampDepth);
		depthNumber.text = clampDepth+" m";

		Debug.Log("Value: " + divemanager.AscentValue);
		float ascentRisk = divemanager.AscentValue / divemanager.MaxAscentValue;
		Debug.Log(ascentRisk);
		ascentSlider.value = ascentRisk;

		if(ascentRisk >= .8f)
		{
			ascentText.text = "EXTREME";
		}
		else if(ascentRisk >= .6f)
		{
			ascentText.text = "High";
		}
		else if(ascentRisk >= .4f)
		{
			ascentText.text = "Moderate";
		}
		else if(ascentRisk >= .2f)
		{
			ascentText.text = "Low";
		}

		ascentText.text = divemanager.AscentRate.ToString();
	}
}

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
	[SerializeField] private TMP_Text compacityText;
	private float currentUITime;
	private DiveManager diveManager;
	private CollectionController collectionController;

	void Awake()
	{
		diveManager = FindObjectOfType<DiveManager>();
		collectionController = FindObjectOfType<CollectionController>();
	}
	void Start()
	{
		currentUITime = uiUpdateRate;
	}

	void Update()
	{
		if(currentUITime >= uiUpdateRate)
		{
			int clampPsi = Mathf.FloorToInt(diveManager.Psi);
			psiNumber.text = "PSI: " + clampPsi;
			psiSlider.value = diveManager.Psi / diveManager.MaxPsi;
			currentUITime = 0f + (currentUITime % uiUpdateRate);
		}
		else
		{
			currentUITime += Time.deltaTime;
		}
	
		int clampDepth = Mathf.FloorToInt(diveManager.Depth);
		clampDepth = Mathf.Abs(clampDepth);
		depthNumber.text = clampDepth+" m";

		float ascentRisk = diveManager.AscentValue / diveManager.MaxAscentValue;
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

		compacityText.text = collectionController.CurrentCompacity.ToString("0.00") + "/" + collectionController.MaxCompacity.ToString("0.00");
	}
}

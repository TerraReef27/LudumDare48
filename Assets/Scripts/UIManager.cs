using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	[SerializeField] private float uiUpdateRate;
	[SerializeField] private Slider psiSlider;
	[SerializeField] private TMP_Text psiNumber;
	[SerializeField] private TMP_Text depthNumber;
	private float currentUITime;
	private DiveManager divemanager;

	void Awake()
	{
		divemanager = FindObjectOfType<DiveManager>();
	}
	void Start()
	{
		currentUITime = 0;
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
	}
}

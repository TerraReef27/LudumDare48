using UnityEngine;

public class BlackoutUI : MonoBehaviour
{
	public void DiveAgain()
	{
		var gm = FindObjectOfType<GameManager>();
		gm.SwitchUI(0);
		gm.TriggerReset();
	}
}

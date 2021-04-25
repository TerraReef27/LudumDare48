using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool paused;

	[SerializeField] private GameObject[] UIs;

	public event OnResetDelegate OnReset;
    public delegate void OnResetDelegate();
	public event OnBlackoutDelegate OnBlackout;
    public delegate void OnBlackoutDelegate();

	public void TriggerReset()
	{
		OnReset?.Invoke();
	}

	public void TriggerBlackout()
	{
		Debug.Log("Blackout");
		OnBlackout?.Invoke();
		TriggerReset();
		SwitchUI(3);
	}

	public void SwitchUI(int uiNum) //I know a key value would make more sense. Just lemme jank this
	{
		paused = uiNum != 0 ? true : false;

		for(int i=0;i<UIs.Length;i++)
		{
			if(i == uiNum)
				UIs[uiNum].SetActive(true);
			else
				UIs[i].SetActive(false);
		}
	}
}

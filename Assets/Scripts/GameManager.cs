using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool paused;

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
		OnBlackout?.Invoke();
	}
}

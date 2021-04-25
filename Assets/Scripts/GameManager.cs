using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public bool paused;

	public event OnResetDelegate OnReset;
    public delegate void OnResetDelegate();

	public void Reset()
	{
		OnReset?.Invoke();
	}
}

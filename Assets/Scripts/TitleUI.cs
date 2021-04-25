using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
	public void PlayBttn()
	{
		SceneManager.LoadScene(1);
	}
	public void QuitBttn()
	{
		Application.Quit();
	}
}

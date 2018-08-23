using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
	public void ChangeSceneToGame()
	{
		SceneManager.LoadScene("Game");
	}

	public void CloseGame()
	{
		Application.Quit();
	}
}

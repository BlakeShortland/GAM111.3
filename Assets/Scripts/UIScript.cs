using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
	public void ChangeSceneToGame()
	{
		SceneManager.LoadScene("Game");
	}

	public void ChangeSceneToReferences()
	{
		SceneManager.LoadScene("References");
	}

	public void ChangeSceneToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void CloseGame()
	{
		Application.Quit();
	}
}

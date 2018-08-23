using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour
{
	bool paused;

	public GameObject pausePanel;

	public GameObject player;

	void Start()
	{
		pausePanel = GameObject.Find("Pause Panel");
		player = GameObject.Find("FPSController");

		Unpause();
	}

	void Update()
	{
		KeyChecks();
	}

	public void KeyChecks ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PauseMenuToggle();
		}
	}

	public void PauseMenuToggle()
	{
		if (paused)
		{
			Pause();
			return;
		}
		Unpause();
	}

	public void Pause()
	{
		player.GetComponent<FirstPersonController>().enabled = false;

		pausePanel.gameObject.SetActive(true);

		Cursor.lockState = CursorLockMode.None;

		Cursor.visible = true;

		paused = false;
	}

	public void Unpause()
	{
		player.GetComponent<FirstPersonController>().enabled = true;

		pausePanel.gameObject.SetActive(false);

		Cursor.lockState = CursorLockMode.Locked;

		Cursor.visible = false;

		paused = true;
	}
}

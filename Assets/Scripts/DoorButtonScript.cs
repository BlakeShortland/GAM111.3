using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonScript : MonoBehaviour
{
	[Header("The door and lights that the button will command")]
	[SerializeField] GameObject door;
	[SerializeField] GameObject[] lights;
	[Space(10)]
	[Header("A timer of 0 means that the timer will not be run, anything higher will dictate how long the door is open for")]
	[SerializeField] float openTimer = 0;
	[Space(10)]

	bool doorOpen = false;
	bool doorLocked = false;

	public void OpenClose()
	{
		if (!doorLocked)
		{
			if (doorOpen)
				Close();
			else
				Open();
		}
	}

	void Open()
	{
		door.gameObject.SetActive(false);
		doorOpen = true;

		if (openTimer > 0)
			StartCoroutine(DoorTimer());
	}

	void Close()
	{
		door.gameObject.SetActive(true);
		doorOpen = false;
	}

	void Lock()
	{
		foreach (GameObject light in lights)
			light.gameObject.GetComponent<Light>().color = Color.red;

		doorLocked = true;
	}

	void Unlock()
	{
		foreach (GameObject light in lights)
			light.gameObject.GetComponent<Light>().color = Color.green;

		doorLocked = false;
	}

	IEnumerator DoorTimer()
	{
		yield return new WaitForSecondsRealtime(openTimer);
		Close();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonScript : MonoBehaviour
{
	[SerializeField] GameObject door;

	bool doorOpen = false;

	public void OpenClose()
	{
		if (doorOpen)
			Close();
		else
			Open();
	}

	void Open()
	{
		door.gameObject.SetActive(false);
		doorOpen = true;
	}

	void Close()
	{
		door.gameObject.SetActive(true);
		doorOpen = false;
	}
}

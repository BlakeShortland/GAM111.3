using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonScript : MonoBehaviour
{
	[Header("The gameobject that this button will command.")]
	[SerializeField] GameObject door;
	[SerializeField] GameObject[] lights;
	[SerializeField] GameObject mirror;
	[Space(10)]
	[Header("A timer of 0 means that the timer will not be run, anything higher will dictate how long the door is open for.")]
	[SerializeField] float openTimer = 0;
	[Space(10)]
	[Header("Door states")]
	[SerializeField] bool doorOpen = false;
	[SerializeField] bool doorLocked = false;

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
		if (door != null)
		{
			door.gameObject.SetActive(false);
			doorOpen = true;

			if (openTimer > 0)
				StartCoroutine(DoorTimer());
		}		
	}

	void Close()
	{
		if (door != null)
		{
			door.gameObject.SetActive(true);
			doorOpen = false;
		}
	}

	public void Lock()
	{
		if (lights.Length != 0)
		{
			foreach (GameObject light in lights)
				light.gameObject.GetComponent<Light>().color = Color.red;

			doorLocked = true;
		}
	}

	public void Unlock()
	{
		if (lights.Length != 0)
		{
			foreach (GameObject light in lights)
				light.gameObject.GetComponent<Light>().color = Color.green;

			doorLocked = false;
		}
	}

	public void Rotate()
	{
		if (mirror != null)
		{
			mirror.transform.RotateAround(transform.position, Vector3.up, 90);
			mirror.transform.localPosition = new Vector3(0, 0, 0);
		}
	}

	IEnumerator DoorTimer()
	{
		yield return new WaitForSecondsRealtime(openTimer);
		Close();
	}
}

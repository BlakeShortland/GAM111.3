using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class DoorButtonScript : MonoBehaviour
{
	[Header("The gameobject that this button will command.")]
	public GameObject door;
	public GameObject stair;
	public GameObject[] lights;
	public GameObject mirror;
	[Space(10)]
	[Header("A timer of 0 means that the timer will not be run, anything higher will dictate how long the door is open for.")]
	[SerializeField] float openTimer = 0;
	[Space(10)]
	[Header("Door states")]
	[SerializeField] bool doorOpen = false;
	[SerializeField] bool doorLocked = false;
	[Space(10)]
	[Header("Does this button tell the ai to progress?")]
	[SerializeField] bool aiTrigger = false;

	AudioSource beeper;

	private void Awake()
	{
		beeper = GetComponent<AudioSource>();
	}

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
		if (aiTrigger)
		{
			GameObject ai = GameObject.FindGameObjectWithTag("AI");
			ai.GetComponent<AICharacterControl>().NextTarget();
			aiTrigger = false;
		}

		if (door != null)
		{
			door.GetComponent<DoorController>().Open();
			doorOpen = true;

			if (openTimer > 0)
				StartCoroutine(DoorTimer());
		}		
	}

	void Close()
	{
		if (door != null)
		{
			door.GetComponent<DoorController>().Close();
			doorOpen = false;
		}
	}

	public void Up()
	{
		if (stair != null)
		{
			stair.GetComponent<StairsController>().Up();
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

	public void Beep()
	{
		beeper.Play();
	}

	IEnumerator DoorTimer()
	{
		yield return new WaitForSecondsRealtime(openTimer);
		Close();
	}
}

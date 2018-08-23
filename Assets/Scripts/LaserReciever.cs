using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;

public class LaserReciever : MonoBehaviour
{
	[SerializeField] GameObject buttonToActivate;
	[SerializeField] bool aiTrigger = false;

	LaserController laserController;

	public bool recieving = false;

	private void Awake()
	{
		laserController = GetComponentInChildren<LaserController>();
	}

	private void FixedUpdate()
	{
		if (buttonToActivate != null)
		{
			if (recieving && buttonToActivate.GetComponent<DoorButtonScript>().door != null)
				buttonToActivate.GetComponent<DoorButtonScript>().Unlock();
			if (!recieving && buttonToActivate.GetComponent<DoorButtonScript>().door != null)
				buttonToActivate.GetComponent<DoorButtonScript>().Lock();

			if (recieving && buttonToActivate.GetComponent<DoorButtonScript>().stair != null)
				buttonToActivate.GetComponent<DoorButtonScript>().Up();
		}
	}

	public void Activate ()
	{
		recieving = true;

		if (aiTrigger)
		{
			GameObject ai = GameObject.FindGameObjectWithTag("AI");
			ai.GetComponent<AICharacterControl>().NextTarget();
			aiTrigger = false;
		}

		if (laserController != null)
		{
			laserController.ActivateLaser(true);
		}
	}

	public void Deactivate()
	{
		recieving = false;

		if (laserController != null)
		{
			laserController.ActivateLaser(false);
		}
	}
}

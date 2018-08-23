using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserReciever : MonoBehaviour
{
	[SerializeField] GameObject buttonToActivate;

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
			if (recieving)
				buttonToActivate.GetComponent<DoorButtonScript>().Unlock();
			if (!recieving)
				buttonToActivate.GetComponent<DoorButtonScript>().Lock();
		}
	}

	public void Activate ()
	{
		recieving = true;
		laserController.ActivateLaser(true);
	}

	public void Deactivate()
	{
		recieving = false;
		laserController.ActivateLaser(false);
	}
}

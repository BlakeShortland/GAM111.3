using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserReciever : MonoBehaviour
{
	[SerializeField] GameObject buttonToActivate;

	public bool recieving = false;

	private void FixedUpdate()
	{
		if (!recieving)
			Deactivate();
	}

	public void Activate ()
	{
		Debug.Log("Issue is here 3");
		recieving = true;
		Debug.Log("Issue is here 4");
		buttonToActivate.GetComponent<DoorButtonScript>().Unlock();
		Debug.Log("Issue is here 5");
	}

	void Deactivate()
	{
		buttonToActivate.GetComponent<DoorButtonScript>().Lock();
	}
}

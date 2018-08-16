using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : MonoBehaviour
{
	[SerializeField] float interactionDistance = 3f;

	Camera cam;

	void Start()
	{
		cam = Camera.main;
	}

	void Update()
	{
		CheckInteraction();
	}

	void CheckInteraction()
	{
		Vector3 origin = cam.transform.position;
		Vector3 direction = cam.transform.forward;

		RaycastHit hit;

		if (Physics.Raycast(origin, direction, out hit, interactionDistance))
		{
			if (hit.transform.tag == "Button")
			{
				if (Input.GetKeyDown(KeyCode.E))
				{
					hit.transform.gameObject.GetComponent<DoorButtonScript>().OpenClose();
					hit.transform.gameObject.GetComponent<DoorButtonScript>().Rotate();
				}
			}
		}
	}
}

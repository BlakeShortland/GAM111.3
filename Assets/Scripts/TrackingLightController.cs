using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingLightController : MonoBehaviour
{
	[SerializeField] GameObject targetToTrack;
	[SerializeField] float rotationSpeed;
	
	void Update ()
	{
		Vector3 targetDir = targetToTrack.transform.position - transform.position;

		float step = rotationSpeed * Time.deltaTime;

		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

		transform.rotation = Quaternion.LookRotation(newDir);
	}
}

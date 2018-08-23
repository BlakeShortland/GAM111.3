using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class LaserController : MonoBehaviour
{
	public bool LaserActive;
	public float updateFrequency = 0.1f;
	public int laserDistance;
	private float timer = 0;
	private LineRenderer myLineRenderer;
	private GameObject hitReciever = null;

	// Use this for initialization
	void Start()
	{
		timer = 0;
		myLineRenderer = gameObject.GetComponent<LineRenderer>();
		StartCoroutine(RedrawLaser());
	}

	// Update is called once per frame
	void Update()
	{
		if (timer >= updateFrequency)
		{
			timer = 0;
			StartCoroutine(RedrawLaser());
		}
		timer += Time.deltaTime;
	}

	IEnumerator RedrawLaser()
	{
		if (LaserActive)
		{
			int laserReflected = 1; //How many times it got reflected
			int vertexCounter = 1; //How many line segments are there
			bool loopActive = true; //Is the reflecting loop active?

			Vector3 laserDirection = transform.forward; //direction of the next laser
			Vector3 lastLaserPosition = transform.position; //origin of the next laser

			myLineRenderer.positionCount = 1;
			myLineRenderer.SetPosition(0, transform.position);
			RaycastHit hit;

			while (loopActive)
			{
				if (Physics.Raycast(lastLaserPosition, laserDirection, out hit, laserDistance) && (hit.transform.gameObject.tag == "Bounce" || hit.transform.gameObject.tag == "Receiver"))
				{
					if (hit.transform.gameObject.tag == "Bounce")
					{
						laserReflected++;
						vertexCounter += 3;
						myLineRenderer.positionCount = vertexCounter;
						myLineRenderer.SetPosition(vertexCounter - 3, Vector3.MoveTowards(hit.point, lastLaserPosition, 0.01f));
						myLineRenderer.SetPosition(vertexCounter - 2, hit.point);
						myLineRenderer.SetPosition(vertexCounter - 1, hit.point);
						lastLaserPosition = hit.point;
						laserDirection = Vector3.Reflect(laserDirection, hit.normal);

						if (hitReciever != null)
							LaserReceived(hitReciever, false);
					}
					if (hit.transform.gameObject.tag == "Receiver")
					{
						laserReflected++;
						vertexCounter++;
						myLineRenderer.positionCount = vertexCounter;
						lastLaserPosition = hit.point;
						Vector3 lastPos = lastLaserPosition;
						myLineRenderer.SetPosition(vertexCounter - 1, lastPos);

						loopActive = false;

						hitReciever = hit.transform.gameObject;
						LaserReceived(hit.transform.gameObject,true);
					}
				}
				else
				{
					laserReflected++;
					vertexCounter++;
					myLineRenderer.positionCount = vertexCounter;
					Vector3 lastPos = lastLaserPosition + (laserDirection.normalized * laserDistance);
					myLineRenderer.SetPosition(vertexCounter - 1, lastPos);

					loopActive = false;

					if (hitReciever != null)
						LaserReceived(hitReciever, false);
				}
			}
		}
		else
		{
			myLineRenderer.positionCount = 2;
			myLineRenderer.SetPosition(0, new Vector3(50.5f, 5f, -10f));
			myLineRenderer.SetPosition(1, new Vector3(50.5f, 5f, -10f));
		}

		yield return new WaitForEndOfFrame();
	}

	void LaserReceived(GameObject receiver, bool state)
	{
		if (state == true)
			receiver.GetComponent<LaserReciever>().Activate();
		if (state == false)
			receiver.GetComponent<LaserReciever>().Deactivate();
	}

	public void ActivateLaser(bool state)
	{
		LaserActive = state;
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class LaserController : MonoBehaviour
{
	public float updateFrequency = 0.1f;
	public int laserDistance;
	public string bounceTag;
	public string spawnedBeamTag;
	private float timer = 0;
	private LineRenderer myLineRenderer;

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
			if (Physics.Raycast(lastLaserPosition, laserDirection, out hit, laserDistance) && ((hit.transform.gameObject.tag == bounceTag)))
			{
				laserReflected++;
				vertexCounter += 3;
				myLineRenderer.positionCount = vertexCounter;
				myLineRenderer.SetPosition(vertexCounter - 3, Vector3.MoveTowards(hit.point, lastLaserPosition, 0.01f));
				myLineRenderer.SetPosition(vertexCounter - 2, hit.point);
				myLineRenderer.SetPosition(vertexCounter - 1, hit.point);
				lastLaserPosition = hit.point;
				Vector3 prevDirection = laserDirection;
				laserDirection = Vector3.Reflect(laserDirection, hit.normal);
			}
			else
			{
				laserReflected++;
				vertexCounter++;
				myLineRenderer.positionCount = vertexCounter;
				Vector3 lastPos = lastLaserPosition + (laserDirection.normalized * laserDistance);
				myLineRenderer.SetPosition(vertexCounter - 1, lastPos);

				loopActive = false;
			}
		}

		yield return new WaitForEndOfFrame();
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
	Vector3 closedPosition;

	float savedSpeed;

	[SerializeField] float speed;

	void Awake()
	{
		closedPosition = transform.position;
		savedSpeed = speed;
	}

	public void Open()
	{
		StopCoroutine("MoveDoor");
		Vector3 endpos = closedPosition + new Vector3(0f, 0f, 3f);
		StartCoroutine("MoveDoor", endpos);
	}

	public void Close()
	{
		StopCoroutine("MoveDoor");
		StartCoroutine("MoveDoor", closedPosition);
	}

	IEnumerator MoveDoor(Vector3 endPos)
	{
		float t = 0f;
		Vector3 startPos = transform.position;
		while (t < 1f)
		{
			t += Time.deltaTime * speed;
			transform.position = Vector3.Slerp(startPos, endPos, t);
			yield return null;
		}
	}
}

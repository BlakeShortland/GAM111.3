using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
	Vector3 upPosition;

	float savedSpeed;

	[SerializeField] float speed;

	void Awake()
	{
		upPosition = transform.position;
		savedSpeed = speed;
	}

	void Start()
	{
		Down();
	}

	public void Down()
	{
		StopCoroutine("MoveStairs");
		Vector3 endpos = upPosition + new Vector3(0f, -10f, 0f);
		StartCoroutine("MoveStairs", endpos);
	}

	public void Up()
	{
		StopCoroutine("MoveStairs");
		StartCoroutine("MoveStairs", upPosition);
	}

	IEnumerator MoveStairs(Vector3 endPos)
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

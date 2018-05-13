using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
	public float rotateSpeed = 0.5f;

	public float maxRotate = 360.0f;
	public float minRotate = 215.5f;
	public float turnWait = 0.5f;
	private float direction = -1.0f;
	private bool turning = true;
	
	// Update is called once per frame
	void Update ()
	{
		if (turning)
		{
			transform.Rotate(0f, 0f, rotateSpeed * direction);

			if (transform.localEulerAngles.z < minRotate || transform.localEulerAngles.z > maxRotate)
			{
				transform.Rotate(0f, 0f, rotateSpeed * -direction);
				turning = false;

				if (turnWait > 0)
					StartCoroutine("CameraWait");
				else
					TurnWaited();
			}
		}
	}

	private void TurnWaited()
	{
		direction *= -1f;
		turning = true;
	}

	IEnumerator CameraWait()
	{
		yield return new WaitForSeconds(turnWait);
		TurnWaited();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardTakedown : MonoBehaviour
{
	private  Guard guardScript;

	void Start()
	{
		guardScript = GetComponentInParent<Guard>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && other.name == "Will")
		{
			Will willScript = other.GetComponent<Will>();
			willScript.SetGuardTarget(guardScript);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player" && other.name == "Will")
		{
			Will willScript = other.GetComponent<Will>();
			willScript.SetGuardTarget(null);
		}
	}
}

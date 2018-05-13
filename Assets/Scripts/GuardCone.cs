using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCone : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			// detection code here
			Debug.Log("other.name");
		}
	}
}

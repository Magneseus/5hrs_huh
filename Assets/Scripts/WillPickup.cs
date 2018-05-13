using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillPickup : MonoBehaviour {

	private Wei weiRef;

	void Start()
	{
		weiRef = GetComponentInParent<Wei>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && other.name == "Will")
		{
			other.GetComponent<Will>().SetWei(weiRef);
		}
	}

}

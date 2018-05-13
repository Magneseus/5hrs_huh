using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wei : PlayerController {

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}

	public void VentDisable()
	{
		PickupDisable();
		GetComponentInChildren<SpriteRenderer>().enabled = false;
		GetComponentInChildren<WillPickup>().enabled = false;
		GetComponentInChildren<BoxCollider2D>().enabled = false;
	}

	public void VentEnable()
	{
		PickupReset();
		GetComponentInChildren<SpriteRenderer>().enabled = true;
		GetComponentInChildren<WillPickup>().enabled = true;
		GetComponentInChildren<BoxCollider2D>().enabled = true;
	}

	public void PickupDisable()
	{
		GetComponent<BoxCollider2D>().enabled = false;
		GetComponent<Rigidbody2D>().simulated = false;
		GetComponent<Movement>().enabled = false;
		GetComponent<Wei>().enabled = false;
	}

	public void PickupReset()
	{
		GetComponent<BoxCollider2D>().enabled = true;
		GetComponent<Rigidbody2D>().simulated = true;
		GetComponent<Movement>().enabled = true;
		GetComponent<Wei>().enabled = true;
	}
}

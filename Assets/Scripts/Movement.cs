﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public float moveForce;
	public float slowForce;
	public float maxSpeed;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	public void Move(float horizontal, float vertical)
	{
		// Horizontal movement
		if (horizontal != 0 && horizontal * rb.velocity.x < maxSpeed)
		{
			rb.AddForce(new Vector2(horizontal * moveForce, 0f));
		}

		// Slowing down
		if (Mathf.Abs(rb.velocity.x) > 0.1f)
		{
			rb.AddForce(new Vector2(Mathf.Sign(rb.velocity.x) * -1f * slowForce, 0f));
		}

		// Stopping
		if (Mathf.Abs(rb.velocity.x) <= 0.1f)
		{
			rb.velocity = new Vector2(0f, rb.velocity.y);
		}
		
		// Capping speed
		if (Mathf.Abs(rb.velocity.x) > maxSpeed)
		{
			rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
		}
	}
}
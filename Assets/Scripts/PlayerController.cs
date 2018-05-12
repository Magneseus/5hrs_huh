using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float moveForce;
	public float slowForce;
	public float maxSpeed;
	public float jumpForce;
	public Transform groundLoc;
	
	private Rigidbody2D rb;
	private bool grounded;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		// Checking for ground
		grounded = Physics2D.Linecast(transform.position, groundLoc.position, 1 << LayerMask.NameToLayer("Ground"));

		// Jumping
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
        }

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

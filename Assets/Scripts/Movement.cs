using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public float moveForce;
	public float slowForce;
	public float maxSpeed;

	private Rigidbody2D rb;
	private bool facingLeft = true;

	private bool ragdoll = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (!ragdoll)
		{
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

	public void Move(float horizontal, float vertical)
	{
		if (horizontal > 0 && facingLeft)
			Flip();
		else if (horizontal < 0 && !facingLeft)
			Flip();

		if (!ragdoll)
		{
			// Horizontal movement
			if (horizontal != 0 && horizontal * rb.velocity.x < maxSpeed)
			{
				rb.AddForce(new Vector2(horizontal * moveForce, 0f));
			}
		}	
	}

	public void Flip()
	{
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
	}

	public void Flip(bool facingLeft_)
	{
		facingLeft = facingLeft_;

		Vector3 theScale = transform.localScale;
        
		if (facingLeft && Mathf.Sign(theScale.x) < 0)
		{
			theScale.x *= -1f;
		}
		else if (!facingLeft && Mathf.Sign(theScale.x) > 0)
		{
			theScale.x *= -1f;
		}

        transform.localScale = theScale;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (ragdoll)
		{
			ragdoll = false;
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			transform.rotation = Quaternion.identity;
		}
	}

	public void Ragdoll()
	{
		ragdoll = true;
		rb.constraints = RigidbodyConstraints2D.None;
	}
}

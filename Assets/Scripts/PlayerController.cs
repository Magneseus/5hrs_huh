using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed;
	
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		// Horizontal movement
		rb.AddRelativeForce(new Vector2(horizontal * speed, 0f));
		
		if (Input.GetAxis("Jump") > 0f)
		{
			// Jump code?
		}
	}
}

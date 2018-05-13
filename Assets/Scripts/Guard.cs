using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
	public Transform wallCheck;

	private Movement move;
	private float direction = -1f;

	// Use this for initialization
	void Start ()
	{
		move = GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		move.Move(direction, 0f);

		// If we're not player controlled, look for walls
		// Goomba movement
		if (wallCheck != null)
		{
			// Checking for ground
			if (Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground")))
			{
				direction *= -1f;
				move.Flip();
			}
		}
	}

	public void Takedown()
	{
		Debug.Log("Takedown");
	}
}

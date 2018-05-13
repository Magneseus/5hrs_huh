using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
	public Transform wallCheck;

	private Movement move;
	private float direction = -1f;
	private bool dead = false;

	// Use this for initialization
	void Start ()
	{
		move = GetComponent<Movement>();

		// Ignore collision with other guards
		GameObject[] guards = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject go in guards)
		{
			Physics2D.IgnoreCollision(go.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!dead)
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
	}

	public void Takedown()
	{
		Debug.Log("Takedown");
		dead = true;

		// Rotate face down
		this.transform.Rotate(0f, 0f, 90f * this.transform.localScale.x);

		// Disable movement script
		move.enabled = false;

		// Disable vision cone
		GetComponentInChildren<GuardCone>().DisableConeSprite();
		GetComponentInChildren<GuardCone>().enabled = false;
		GetComponentInChildren<PolygonCollider2D>().enabled = false;

		// Disable takedown script
		GetComponentInChildren<GuardTakedown>().enabled = false;

		// Disable collision with players
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject go in players)
		{
			Physics2D.IgnoreCollision(go.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
		}
	}

	public bool IsDead()
	{
		return dead;
	}
}

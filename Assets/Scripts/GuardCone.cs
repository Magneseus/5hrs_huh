using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCone : MonoBehaviour {
	public SpriteRenderer coneSprite;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Bounds playerBounds = other.bounds;
			Vector3 topRight = playerBounds.min;
			topRight.x += playerBounds.extents.x;
			Vector3 bottomLeft = playerBounds.min;
			bottomLeft.y += playerBounds.extents.y;

			int checkCount = 4;

			// LOS check
			if (Physics2D.Linecast(transform.position, playerBounds.min, 1 << LayerMask.NameToLayer("Ground")))
				checkCount--;
			if (Physics2D.Linecast(transform.position, playerBounds.max, 1 << LayerMask.NameToLayer("Ground")))
				checkCount--;
			if (Physics2D.Linecast(transform.position, bottomLeft, 1 << LayerMask.NameToLayer("Ground")))
				checkCount--;
			if (Physics2D.Linecast(transform.position, topRight, 1 << LayerMask.NameToLayer("Ground")))
				checkCount--;
			
			if (checkCount == 0)
				return;

			// detection code here
			Debug.Log("other.name");
		}
	}

	public void DisableConeSprite()
	{
		coneSprite.enabled = false;
	}

	public void EnableConeSprite()
	{
		coneSprite.enabled = true;
	}
}

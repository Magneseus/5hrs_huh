using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Will : PlayerController
{
	public Transform weiPlacement;
	private Vector3 oldPosition;

	private Guard guardTarget;
	private Wei weiPickup;
	private bool weiPickedUp = false;


	private Vector3 throwArea = new Vector3(-0.78f, 0.381f, 0f);
	public float throwStrength = 1.0f;

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();

		if (playerSelected)
		{
			if (Input.GetKeyDown(KeyCode.F) && guardTarget != null)
			{
				if (!guardTarget.IsDead())
					guardTarget.Takedown();
			}
			else if (Input.GetKeyDown(KeyCode.F) && weiPickup != null && !weiPickedUp)
			{
				PickupWei();
			}
			else if (Input.GetKeyDown(KeyCode.F) && weiPickup != null && weiPickedUp)
			{
				ThrowWei();
			}

			if (Input.GetKeyDown(KeyCode.S) && weiPickedUp)
			{
				DropWei();
			}
		}
	}

	public void SetGuardTarget(Guard guard)
	{
		guardTarget = guard;
	}

	public void SetWei(Wei wei)
	{
		weiPickup = wei;

		if (wei == null && !weiPickedUp)
		{
			disableJump = false;
		}
	}

	public bool IsWeiPickedUp()
	{
		return weiPickedUp;
	}

	private void ThrowWei()
	{
		float throwDirection = Mathf.Sign(this.transform.localScale.x);

		DropWei();

		// Place wei
		Vector3 wp = throwArea;
		wp.x *= throwDirection;
		Vector3 throwVec = new Vector3(wp.x, wp.y, wp.z);
		wp = transform.position + wp;
		weiPickup.transform.position = wp;
		
		// Generate throw vector
		throwVec = throwVec.normalized;

		// Apply force to wei
		weiPickup.GetComponent<Rigidbody2D>().AddForceAtPosition(throwVec * throwStrength, weiPickup.transform.position + (Vector3.up * 0.5f));
		weiPickup.GetComponent<Movement>().Ragdoll();
		
		weiPickedUp = false;
		disableJump = false;
	}

	private void PickupWei()
	{
		weiPickup.PickupDisable();
		oldPosition = weiPickup.transform.position - this.transform.position;

		weiPickup.transform.parent = weiPlacement;
		weiPickup.transform.localPosition = new Vector3(0,0,0);
		weiPickup.transform.localRotation = Quaternion.identity;
		weiPickup.transform.localScale = new Vector3(1,1,1);

		weiPickedUp = true;
		disableJump = true;
	}

	private void DropWei()
	{
		weiPickup.PickupReset();

		weiPickup.transform.parent = null;
		weiPickup.transform.position = this.transform.position + oldPosition;
		weiPickup.transform.rotation = Quaternion.identity;
		weiPickup.GetComponent<Movement>().Flip((Mathf.Sign(this.transform.localScale.x) > 0 ? true : false));

		weiPickedUp = false;
		disableJump = false;
	}
}

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
				guardTarget.Takedown();
			}
			else if (Input.GetKeyDown(KeyCode.F) && weiPickup != null)
			{
				PickupWei();
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
	}

	public bool IsWeiPickedUp()
	{
		return weiPickedUp;
	}

	private void ThrowWei()
	{
		float throwDirection = -Mathf.Sign(this.transform.localScale.x);

		
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
	}

	private void DropWei()
	{
		weiPickup.PickupReset();

		weiPickup.transform.parent = null;
		weiPickup.transform.position = this.transform.position + oldPosition;
		weiPickup.transform.rotation = Quaternion.identity;
		weiPickup.GetComponent<Movement>().Flip((Mathf.Sign(this.transform.localScale.x) > 0 ? true : false));

		weiPickedUp = false;
	}
}

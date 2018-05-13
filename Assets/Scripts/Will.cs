using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Will : PlayerController
{
	private Guard guardTarget;

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();

		if (Input.GetKeyDown(KeyCode.F) && guardTarget != null)
		{
			guardTarget.Takedown();
		}
	}

	public void SetGuardTarget(Guard guard)
	{
		guardTarget = guard;
	}
}

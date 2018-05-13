﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
	private Camera mainCam;
	private Will will;
	private Wei wei;
	private PlayerController activeCharacter;

	// Use this for initialization
	void Start ()
	{
		mainCam = Camera.main;

		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject go in players)
		{
			if (go.name == "Will")
				will = go.GetComponent<Will>();
			else if (go.name == "Wei")
				wei = go.GetComponent<Wei>();
		}

		SetPlayerWei();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			SwitchPlayers();
		}
	}

	void SwitchPlayers()
	{
		if (activeCharacter.name == "Will")
			SetPlayerWei();
		else
			SetPlayerWill();
	}

	void SetPlayerWill()
	{
		activeCharacter = will;
		will.SetPlayerSelected(true);
		wei.SetPlayerSelected(false);

		Camera.main.transform.parent = will.transform;
		Camera.main.transform.localPosition = new Vector3(0f, 0f, -10f);
	}

	void SetPlayerWei()
	{
		activeCharacter = wei;
		will.SetPlayerSelected(false);
		wei.SetPlayerSelected(true);

		Camera.main.transform.parent = wei.transform;
		Camera.main.transform.localPosition = new Vector3(0f, 0f, -10f);
	}
}

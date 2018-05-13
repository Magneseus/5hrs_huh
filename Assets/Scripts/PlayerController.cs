﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float jumpForce;
	public Transform groundLoc;
	public PhysicsMaterial2D airMaterial;

    private Interactable interactable = null;
    public bool interacting = false;
	public bool nodeMoved = false;
	
	private Rigidbody2D rb;
	private Movement move;
	private bool grounded;

	// Use this for initialization
	public virtual void Start () {
		rb = GetComponent<Rigidbody2D>();
		move = GetComponent<Movement>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");
        
        if(Input.GetKeyUp(KeyCode.Q) && interactable != null)
        {
            Debug.Log("interacting with " + interactable);
            interacting = true;
            interactable.Interact(this);
        }

		// Horizontal Movement
		move.Move(horizontal, 0f);

		// Checking for ground
		grounded = Physics2D.Linecast(transform.position, groundLoc.position, 1 << LayerMask.NameToLayer("Ground"));
		
		// Resetting the physics material
		if (grounded)
			rb.sharedMaterial = null;

		// Jumping
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
			rb.sharedMaterial = airMaterial;
        }
	}

	void FixedUpdate()
	{
		
	}

    public Interactable GetInteractable()
    {
        return interactable;
    }

    public void SetInteractable(Interactable interact)
    {
        interactable = interact;
    }
}

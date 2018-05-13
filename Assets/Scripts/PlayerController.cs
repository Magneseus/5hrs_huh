using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float jumpForce;
	public Transform groundLoc;
	public PhysicsMaterial2D airMaterial;

    private Interactable interactable = null;
	[HideInInspector]
    public bool interacting = false;
	[HideInInspector]
	public bool nodeMoved = false;
	
	private Rigidbody2D rb;
	private Movement move;
	private bool grounded;
	protected bool disableJump = false;

	protected bool playerSelected = false;
	
	private MovementNode movementNode = null;
	private Camera mainCam;

	// Use this for initialization
	public virtual void Start () {
		rb = GetComponent<Rigidbody2D>();
		move = GetComponent<Movement>();
		mainCam = Camera.main;

		// Ignore collisions with other players
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject go in players)
		{
			Physics2D.IgnoreCollision(go.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
		}
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if (playerSelected)
		{
			float vertical = Input.GetAxis("Vertical");
			float horizontal = Input.GetAxis("Horizontal");
			
			if(Input.GetKeyUp(KeyCode.Q) && interactable != null)
			{
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
			if (!disableJump && Input.GetButtonDown("Jump") && grounded)
			{
				rb.AddForce(new Vector2(0f, jumpForce));
				rb.sharedMaterial = airMaterial;
			}
		}
	}

    public Interactable GetInteractable()
    {
        return interactable;
    }

    public void SetInteractable(Interactable interact)
    {
        interactable = interact;
    }

	public void SetPlayerSelected(bool playerSelected)
	{
		this.playerSelected = playerSelected;
	}
	
	public virtual void SetCameraParent(Camera cam)
	{
		if (movementNode == null)
		{
			cam.transform.parent = this.transform;
			cam.transform.localPosition = new Vector3(0,0,-10f);
		}	
		else
		{
			cam.transform.parent = movementNode.transform;
			cam.transform.localPosition = new Vector3(0,0,-10f);
		}
	}
	
	public void SetMovementNode(MovementNode movementNode)
	{
		if (movementNode != null)
		{
			mainCam.transform.parent = movementNode.transform;
			mainCam.transform.localPosition = new Vector3(0,0,-10f);
		}
		else
		{
			mainCam.transform.parent = this.transform;
			mainCam.transform.localPosition = new Vector3(0,0,-10f);
		}
		
		this.movementNode = movementNode;
	}
	
	public bool IsPlayerSelected()
	{
		return playerSelected;
	}
}

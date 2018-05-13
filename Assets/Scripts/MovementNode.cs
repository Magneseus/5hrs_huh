using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementNode : Interactable {

    public MovementNode north = null;
    public MovementNode east = null;
    public MovementNode south = null;
    public MovementNode west = null;

    public SpriteRenderer chevron;

    private PlayerController contains;

    public override void Start()
    {
        base.Start();
        chevron.enabled = false;
    }

    public override void Interact(PlayerController pc)
    {
        // disable the player controller
        SetPlayer(pc);

        base.Interact(contains);
        contains.transform.gameObject.SetActive(false);
        contains.SetMovementNode(this);
    }

    public void SetPlayer(PlayerController pc)
    {
        contains = pc;
        if (contains)
        {
            contains.transform.position = this.transform.position;
            chevron.enabled = true;
        }
        else
        {
            chevron.enabled = false;
        }
    }

    public virtual void LeaveNodeMap()
    {
        contains.transform.gameObject.SetActive(true);
        contains.SetMovementNode(null);
        this.SetPlayer(null);
    }

    public virtual void Update()
    {
        if(!contains || !contains.IsPlayerSelected())
        {
            return;
        }

        MovementNode newNode = null;

        // leave spot
        if(Input.GetKeyUp(KeyCode.Q) && !contains.interacting)
        {
            LeaveNodeMap(); 
            return;
        }
        // moving up
        else if (!contains.nodeMoved && Input.GetButtonDown("Vertical") && Mathf.Sign(Input.GetAxis("Vertical")) > 0) 
        {
            newNode = north;
            contains.nodeMoved = true;
        }
        // moving down
        else if (!contains.nodeMoved && Input.GetButtonDown("Vertical") && Mathf.Sign(Input.GetAxis("Vertical")) < 0)
        {
            newNode = south;
            contains.nodeMoved = true;
        }
        // moving right
        else if (!contains.nodeMoved && Input.GetButtonDown("Horizontal") && Mathf.Sign(Input.GetAxis("Horizontal")) > 0)
        {
            newNode = east;
            contains.nodeMoved = true;
        }
        // moving left
        else if (!contains.nodeMoved && Input.GetButtonDown("Horizontal") && Mathf.Sign(Input.GetAxis("Horizontal")) < 0)
        {
            newNode = west;
            contains.nodeMoved = true;
        }

        contains.interacting = false;

        if (newNode)
        {
            newNode.SetPlayer(contains);
            contains.SetMovementNode(newNode);
            this.SetPlayer(null);
        }

        StartCoroutine("NodeWait");
    }

    IEnumerator NodeWait()
    {
        yield return new WaitForEndOfFrame();
        
        if (contains)
            contains.nodeMoved = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            pc.SetInteractable(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController pc = collision.GetComponent<PlayerController>();
            pc.SetInteractable(null);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementNode : Interactable {

    private MovementNode north = null;
    private MovementNode east = null;
    private MovementNode south = null;
    private MovementNode west = null;

    private PlayerController contains;

    public override void Interact(PlayerController pc)
    {
        // disable the player controller
        base.Interact(pc);
        SetPlayer(pc);
        contains.enabled = false;
    }

    public void SetPlayer(PlayerController pc)
    {
        contains = pc;
        contains.transform.position = this.transform.position;
    }

    public void Update()
    {
        Debug.Log("mn update");
        MovementNode newNode = null;

        // leave spot
        if(Input.GetKeyUp(KeyCode.Q))
        {
            contains.enabled = true;
            contains = null;
            return;
        }
        // moving up
        else if(Input.GetButtonUp("Vertical") && Mathf.Sign(Input.GetAxis("Vertical")) > 0) 
        {
            newNode = north;
        }
        // moving down
        else if (Input.GetButtonUp("Vertical") && Mathf.Sign(Input.GetAxis("Vertical")) < 0)
        {
            newNode = south;
        }
        // moving right
        else if (Input.GetButtonUp("Horizontal") && Mathf.Sign(Input.GetAxis("Horizontal")) > 0)
        {
            newNode = east;
        }
        // moving left
        else if (Input.GetButtonUp("Horizontal") && Mathf.Sign(Input.GetAxis("Horizontal")) < 0)
        {
            newNode = west;
        }

        if (newNode)
        {
            newNode.SetPlayer(contains);
            this.SetPlayer(null);
        }
    }
}

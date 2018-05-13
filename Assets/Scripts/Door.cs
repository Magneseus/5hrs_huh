using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {
    public bool open = false;
    //public SpriteRenderer openDoor;
    //public SpriteRenderer closedDoor;
    public Collider2D col;

    public override void Start()
    {
        base.Start();
        SetPower(open);
        //openDoor.enabled = open;
        //closedDoor.enabled = !open;
        //if (open)
        //{
           // col.enabled = false;
        //}
    }

    public override void Interact(PlayerController pc)
    {
        open = !open;
        SetPower(open);
    }

    public override void PowerOn()
    {
        base.PowerOn();

        open = true;
        col.enabled = false;
    }

    public override void PowerOff()
    {
        base.PowerOff();

        open = false;
        col.enabled = true;

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

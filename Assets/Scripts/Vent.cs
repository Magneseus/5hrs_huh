using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MovementNode {

    public override void Interact(PlayerController pc)
    {
        base.Interact(pc);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.name == "Wei")
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

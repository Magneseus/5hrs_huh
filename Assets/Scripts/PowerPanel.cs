using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPanel : Interactable
{
    public List<Interactable> powers;
    public bool on = false;

    // Use this for initialization
    public override void Start () {
        SetPower(on);
        if(powers != null)
        {
            foreach(Interactable i in powers)
            {
                i.SetPower(on);
            }
        }
	}

    public override void Interact(PlayerController pc)
    {
        on = !on;

        foreach(Interactable i in powers)
        {
            i.SetPower(!i.powered); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.name == "Wei")
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

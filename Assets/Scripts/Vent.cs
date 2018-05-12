using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MovementNode {

    public static GameObject ventMap = null;

    public override void Start()
    {
        base.Start();
        if (!ventMap)
        {
            ventMap = GameObject.FindGameObjectWithTag("Vent Overlay");
            ventMap.SetActive(false);
        }
    }

    public override void LeaveNodeMap()
    {
        base.LeaveNodeMap();
        ventMap.SetActive(false);
    }

    public override void Interact(PlayerController pc)
    {
        base.Interact(pc);
        ventMap.SetActive(true);
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

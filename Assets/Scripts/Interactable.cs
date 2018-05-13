using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    public bool powered = false;
    public SpriteRenderer poweredSprite;
    public SpriteRenderer unpoweredSprite;

    public virtual void Start()
    {
        if(powered)
        {
            if (poweredSprite != null)
                poweredSprite.enabled = true;

            if (unpoweredSprite != null)
                unpoweredSprite.enabled = false;
        }
        else
        {
            if(poweredSprite != null)
                poweredSprite.enabled = false;

            if(unpoweredSprite != null)
                unpoweredSprite.enabled = true;
        }
    }

    public virtual void SetPower(bool p)
    {
        if(p)
        {
            PowerOn();
        }
        else
        {
            PowerOff();
        }
    }

    public virtual void PowerOn()
    {
        powered = true;
        if(poweredSprite != null)
            poweredSprite.enabled = true;

        if(unpoweredSprite != null)
            unpoweredSprite.enabled = false;
    }

    public virtual void PowerOff()
    {
        powered = false;
        if (poweredSprite != null)
            poweredSprite.enabled = false;

        if (unpoweredSprite != null)
            unpoweredSprite.enabled = true;
    }

    public virtual void Interact(PlayerController pc)
    {
    }
}

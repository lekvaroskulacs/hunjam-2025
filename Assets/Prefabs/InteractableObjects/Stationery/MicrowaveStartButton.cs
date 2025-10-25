using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveStartButton : InteractableBase
{
    public override void Interact()
    {
        Debug.Log("Micro Started!!!");
    }

    public override void Select()
    {
        base.Select();

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.red;
        }
    }

    public override void DeSelect()
    {
        base.DeSelect();

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.black;
        }
    }
}

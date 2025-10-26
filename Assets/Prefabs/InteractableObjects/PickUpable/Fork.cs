using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fork : InteractableBase
{
    public override void Interact()
    {
        PickUp();
        Player.Instance.IsHoldingForkSpoon = true;
    }

    public override void Select()
    {
        base.Select();

        //SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        //if (sprite != null)
        //{
        //    sprite.color = Color.red;
        //}
    }

    public override void DeSelect()
    {
        base.DeSelect();

        //SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        //if (sprite != null)
        //{
        //    sprite.color = Color.black;
        //}
    }
}

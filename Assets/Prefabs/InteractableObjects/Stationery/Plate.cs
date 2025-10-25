using UnityEngine;

public class Plate : InteractableBase
{
    public override void Interact()
    {
        Debug.Log("Game Finished!!!");
        var control = GameObject.FindWithTag("Player").GetComponent<Control>();
        control.GameFinished();
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

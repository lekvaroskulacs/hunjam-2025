using UnityEngine;

public class Bun : InteractableBase
{
    public override void Interact()
    {
        Debug.Log("Player bunned!");
        var control = GameObject.FindWithTag("Player").GetComponent<Control>();
        control.Bunned();
    }
}

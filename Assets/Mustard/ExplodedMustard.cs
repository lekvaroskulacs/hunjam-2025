using UnityEngine;

public class ExplodedMustard : InteractableBase
{
    public override void Interact()
    {
        Debug.Log("Player mustarded!");
        var control = GameObject.FindWithTag("Player").GetComponent<Control>();
        control.Mustarded();
    }
}

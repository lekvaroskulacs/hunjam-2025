using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveHandle : InteractableBase
{
    public bool hasFork = false;
    
    public override void Interact()
    {
        Debug.Log("Microwave Handle Turned!!!");
    }
}

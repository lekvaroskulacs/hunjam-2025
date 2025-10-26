using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveHandle : InteractableBase
{
    public bool hasFork = false;

    [SerializeField] GameObject _forkSpoon;
    
    public override void Interact()
    {
        if (Player.Instance.IsHoldingForkSpoon)
        {
            hasFork = true;

            _forkSpoon.GetComponent<InteractableBase>().Drop();
            Destroy(_forkSpoon);
        }

        Debug.Log("Microwave Handle Turned!!!");
    }
}

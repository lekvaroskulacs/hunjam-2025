using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveHandle : InteractableBase
{
    public bool hasFork = false;

    [SerializeField] GameObject WhiteMicroScreen;

    [SerializeField] GameObject _forkSpoon;
    
    public override void Interact()
    {
        if (Player.Instance.IsHoldingForkSpoon)
        {
            hasFork = true;

            _forkSpoon.GetComponent<InteractableBase>().Drop();
            Destroy(_forkSpoon);

            WhiteMicroScreen.gameObject.SetActive(false);
        }
        // Show nned for Spoon

        Debug.Log("Microwave Handle Turned!!!");
    }
}

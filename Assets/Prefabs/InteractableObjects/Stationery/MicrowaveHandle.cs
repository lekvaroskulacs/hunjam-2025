using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveHandle : InteractableBase
{
    public bool hasFork = false;

    [SerializeField] GameObject WhiteMicroScreen;

    [SerializeField] GameObject _forkSpoon;

    private bool _isPorpuseFulfilled;
    public override void Interact()
    {
        if (_isPorpuseFulfilled) return;

        if (Player.Instance.IsHoldingForkSpoon)
        {
            hasFork = true;

            _forkSpoon.GetComponent<InteractableBase>().Drop();
            Destroy(_forkSpoon);
            Player.Instance.IsHoldingForkSpoon = false;

            WhiteMicroScreen.gameObject.SetActive(false);
            base.DeSelect();
            _isPorpuseFulfilled = true;
        }
        // Show nned for Spoon

        Debug.Log("Microwave Handle Turned!!!");
    }

    public override void Select()
    {
        if (_isPorpuseFulfilled) return;
        base.Select();

        //SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        //if (sprite != null)
        //{
        //    sprite.color = Color.red;
        //}
    }

    public override void DeSelect()
    {
        if (_isPorpuseFulfilled) return;
        base.DeSelect();

        //SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        //if (sprite != null)
        //{
        //    sprite.color = Color.black;
        //}
    }
}

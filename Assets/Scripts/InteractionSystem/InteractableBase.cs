using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    public abstract void Interact();

    public virtual void Select()
    {
        Debug.Log("SSSSSSSSSSSSSSSSSSSSSSSSSSSSelect");
    }

    public virtual void DeSelect()
    {
        Debug.Log("DEEEEEEEEEEESelect");
    }
}

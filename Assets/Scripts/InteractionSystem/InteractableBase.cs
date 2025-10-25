using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    public bool IsPickupable = false;

    private bool _isBeingCarried = false;

    public abstract void Interact();

    public virtual void Select()
    {
        Debug.Log("SSSSSSSSSSSSSSSSSSSSSSSSSSSSelect");
    }

    public virtual void DeSelect()
    {
        Debug.Log("DEEEEEEEEEEESelect");
    }

    public virtual void PickUp()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
            collider.enabled = false;

        // Make rigidbody kinematic
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.isKinematic = true;
        }

        _isBeingCarried = true;
    }

    public virtual void Drop()
    {
        transform.position = Player.Instance.transform.position;

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
            collider.enabled = true;

        // Make rigidbody kinematic
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.isKinematic = false;
        }

        _isBeingCarried = false;
    }

    private void Update()
    {
        if (_isBeingCarried)
        {
            transform.position = Player.Instance.transform.position;
        }
    }
}

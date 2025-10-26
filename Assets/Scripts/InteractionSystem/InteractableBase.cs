using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    public bool IsPickupable = false;

    private bool _isBeingCarried = false;

    [SerializeField] private GameObject _promptBubble; 

    public abstract void Interact();

    public virtual void Select()
    {
        if (_promptBubble == null)
        {
            Debug.LogWarning("No prompt Bubble was assigned to Interactable");
        }
        else
        {
            _promptBubble.SetActive(true);
        }
    }

    public virtual void DeSelect()
    {
        if (_promptBubble == null)
        {
            Debug.LogWarning("No prompt Bubble was assigned to Interactable");
        }
        else
        {
            _promptBubble.SetActive(false);
        }
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
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
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
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        _isBeingCarried = false;
    }

    private void Update()
    {
        if (_isBeingCarried)
        {
            transform.position = GameObject.FindWithTag("Arm").transform.position;
        }
    }
}

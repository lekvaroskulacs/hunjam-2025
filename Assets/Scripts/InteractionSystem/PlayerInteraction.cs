using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float _interactionRadius = 2f;

    private InteractableBase _currentInteractable;

    void Update()
    {
        FindClosestInteractable();

        
        if (_currentInteractable != null && Input.GetKeyDown(KeyCode.Space))
        {
            _currentInteractable.Interact();
        }
    }

    private void FindClosestInteractable()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _interactionRadius);

        InteractableBase closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out InteractableBase interactable))
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = interactable;
                }
            }
        }

        if (closest != _currentInteractable)
        {
            if (_currentInteractable != null)
                _currentInteractable.DeSelect();

            _currentInteractable = closest;

            if (_currentInteractable != null)
                _currentInteractable.Select();
        }

        if (closest == null && _currentInteractable != null)
        {
            _currentInteractable.DeSelect();
            _currentInteractable = null;
        }
    }
}

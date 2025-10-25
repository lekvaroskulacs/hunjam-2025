using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingTerrain : MonoBehaviour
{
    // The collider representing the solid part of the terrain (the one to enable/disable collision with)
    // If left null the script will try to find a non-trigger Collider2D in parent objects.
    public Collider2D terrainCollider;

    // Name of the character GameObject (adjust if different)
    public string characterObjectName = "Character";

    private void Awake()
    {
        if (terrainCollider == null)
        {
            // find first non-trigger Collider2D in parents
            var parentColliders = GetComponentsInParent<Collider2D>();
            foreach (var c in parentColliders)
            {
                if (c != null && !c.isTrigger)
                {
                    terrainCollider = c;
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;
        if (other.gameObject.name != characterObjectName) return;
        if (terrainCollider == null)
        {
            Debug.LogWarning("JumpingTerrain: terrainCollider not set and none found in parents.");
            return;
        }

        // Decide based on this trigger's name. Attach this script to the trigger GameObjects (UnderTrigger/UpperTrigger)
        if (this.gameObject.name == "UnderTrigger")
        {
            Debug.Log("JumpingTerrain: Character entered UnderTrigger, disabling collision with terrain.");
            // disable collision between the character's collider that triggered this event and the terrain collider
            Physics2D.IgnoreCollision(other, terrainCollider, true);
        }
        else if (this.gameObject.name == "UpperTrigger")
        {
            // enable collision
            Physics2D.IgnoreCollision(other, terrainCollider, false);
        }
    }

    // Optional: also handle exits to be safe (re-enable when character leaves upper trigger)
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == null) return;
        if (other.gameObject.name != characterObjectName) return;
        if (terrainCollider == null) return;

        // If character leaves UnderTrigger, re-enable collision to avoid permanently ignoring
        if (this.gameObject.name == "UnderTrigger")
        {
            Physics2D.IgnoreCollision(other, terrainCollider, false);
        }
    }
}

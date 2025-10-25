using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float launchForce = 20f; // How strong the upward push is

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.attachedRigidbody;

            if (rb != null)
            {
                // Reset vertical velocity before applying force
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

                // Apply upward force
                rb.AddForce(Vector2.up * launchForce, ForceMode2D.Impulse);

                Debug.Log("Player launched upward!");
            }
        }
    }
}

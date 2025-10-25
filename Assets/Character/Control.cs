using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    [SerializeField] private Rigidbody2D rigidbody;

    private Vector2 velocity;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        velocity.x = moveInput * moveSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        rigidbody.velocity = new Vector2(velocity.x, rigidbody.velocity.y);
    }

    private void Jump()
    {
        rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
}

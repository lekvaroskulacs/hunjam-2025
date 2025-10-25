using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D rigidBody;

    private Vector2 velocity;

    private Grounded groundedChecker;

    private void Awake()
    {
        groundedChecker = GetComponentInChildren<Grounded>();
    }

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

        rigidBody.linearVelocity = new Vector2(velocity.x, rigidBody.linearVelocity.y);
    }

    private void Jump()
    {
        if (!groundedChecker.isGrounded)
            return;

        rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cockroach"))
        {
            HitByCockroach(collision.gameObject);
        }
    }

    void HitByCockroach(GameObject cockroach)
    {
        Debug.Log("I died!");
    }
}

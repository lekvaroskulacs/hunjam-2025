using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Control : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float turnDuration = 0.2f;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private RectTransform canvasTransform;
    [SerializeField] private Animator canvasAnim;
    [SerializeField] private ResultScreen resultScreen;

    [SerializeField] private GameObject ketchupEffect;

    private Vector2 velocity;

    private Grounded groundedChecker;
    private Animator anim;

    private float facingDirection = 1f;
    private bool isTurning = false;

    public bool ketchuped = false;
    public bool mustarded = false;
    public bool bunned = false;

    private void Awake()
    {
        groundedChecker = GetComponentInChildren<Grounded>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovement();
        canvasTransform.position = transform.position;
        if (groundedChecker.isGrounded)
        {
            anim.SetBool("Ground", true);
        }
        else
        {
            anim.SetBool("Ground", false);
        }
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        velocity.x = moveInput * moveSpeed;
        anim.SetFloat("Horizontal", Mathf.Abs(velocity.x));

        if (moveInput < 0 && !isTurning)
        {
            facingDirection = 1f;
            StartCoroutine(Turn());
        }
        else if (moveInput > 0 && !isTurning)
        {
            facingDirection = -1f;
            StartCoroutine(Turn());
        }

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
        anim.SetBool("Ground", false);
        anim.SetTrigger("Jumping");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cockroach"))
        {
            HitByCockroach(collision.gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        ketchupEffect.SetActive(true);
    }

    void HitByCockroach(GameObject cockroach)
    {
        Debug.Log("I died!");
        anim.SetTrigger("Dead");
        canvasAnim.SetTrigger("CloseAnim");
    }

    IEnumerator Turn()
    {
        isTurning = true;
        float startRotationY = transform.rotation.eulerAngles.y;
        float targetRotationY = facingDirection == 1f ? 0f : 180f;
        float elapsedTime = 0f;

        while (elapsedTime < turnDuration)
        {
            float newRotationY = Mathf.Lerp(startRotationY, targetRotationY, elapsedTime / turnDuration);
            transform.rotation = Quaternion.Euler(0f, newRotationY, 0f);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.rotation = Quaternion.Euler(0f, targetRotationY, 0f);
        isTurning = false;
    }

    public void GameFinished()
    {
        canvasAnim.SetTrigger("CloseAnim");
        Invoke(nameof(Results), 2f);
    }

    public void Results()
    {
        resultScreen.gameObject.SetActive(true);
        resultScreen.eating.Play();
        resultScreen.CheckResult(ketchuped, mustarded, bunned);
    }
}

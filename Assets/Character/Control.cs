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
    [SerializeField] private GameObject mustardEffect;
    [SerializeField] private GameObject bunFrontEffect;
    [SerializeField] private GameObject bunBackEffect;

    private Vector2 velocity;

    private Grounded groundedChecker;
    private Animator anim;

    private float facingDirection = 1f;
    private bool isTurning = false;
    private bool isDead = false;

    public bool ketchuped = false;
    public bool mustarded = false;
    public bool bunned = false;

    private void Awake()
    {
        groundedChecker = GetComponentInChildren<Grounded>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        canvasAnim.SetTrigger("OpenAnim");
    }
    

    private void Update()
    {
        if (isDead)
            return;
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
        if (Player.Instance.IsHoldingForkSpoon)
        {
            anim.SetBool("Carry", true);
        }
        else
        {
            anim.SetBool("Carry", false);
        }
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        velocity.x = moveInput * moveSpeed;
        anim.SetFloat("Horizontal", Mathf.Abs(velocity.x));

        //if(SoundManager.Instance != null)
        //{
        //    if (moveInput != 0 && groundedChecker.isGrounded)
        //    {
        //            SoundManager.Instance.PlaySFX("player_walk");
        //    }
        //}

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

        Debug.Log("Player jumped.");
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX("player_jump");
        }

        rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        anim.SetBool("Ground", false);
        if (anim.GetBool("Carry"))
            return;

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
        ketchuped = true;
    }

    void HitByCockroach(GameObject cockroach)
    {
        Debug.Log("I died!");
        Die();
    }

    public void Die()
    {
        anim.SetTrigger("Dead");
        canvasAnim.SetTrigger("CloseAnim");
        isDead = true;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        Invoke(nameof(ReloadScene), 2f);
    }

    void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
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
    public void Mustarded()
    {
        mustardEffect.SetActive(true);
        mustarded = true;
    }

    public void Bunned()
    {
        bunFrontEffect.SetActive(true);
        bunBackEffect.SetActive(true);
        bunned = true;
    }
}

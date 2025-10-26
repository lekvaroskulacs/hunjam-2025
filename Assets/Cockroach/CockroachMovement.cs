using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockroachMovement : MonoBehaviour
{
    [SerializeField] Transform start;
    [SerializeField] Transform end;

    [SerializeField] float speed = 2f;
    [SerializeField] float turnDuration = 0.2f;
    [SerializeField] float disappearDuration = 1f;
    [SerializeField] float disappearRate = 0.5f;

    [SerializeField] float targetHeightForDeath = 0.5f;


    BoxCollider2D boxCollider;
    private Animator anim;
    private bool isTurning = false;
    private float facingDirection = 1f;
    private bool dead = false;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (dead) return;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, end.position, step);
        anim.SetFloat("Horizontal", speed);

        if (!isTurning && transform.position.x < end.position.x)
        {
            facingDirection = -1f;
            StartCoroutine(Turn());
        }
        else if (!isTurning && transform.position.x > end.position.x)
        {
            facingDirection = 1f;
            StartCoroutine(Turn());
        }

        if (Vector3.Distance(transform.position, end.position) < 0.001f)
        {
            Transform temp = start;
            start = end;
            end = temp;
        }
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

    private void OnParticleCollision(GameObject other)
    {
        if (dead) return;
        Debug.Log("Dead cockroach!");
        dead = true;
        anim.enabled = false;
        StartCoroutine(TurnUpsideDown());
    }

    IEnumerator TurnUpsideDown()
    {
        isTurning = true;
        float startRotationX = transform.rotation.eulerAngles.x;
        float targetRotationX = 180f;
        float targetHeight = transform.position.y + targetHeightForDeath * transform.lossyScale.y;
        float elapsedTime = 0f;

        while (elapsedTime < turnDuration)
        {
            float newRotationX = Mathf.Lerp(startRotationX, targetRotationX, elapsedTime / turnDuration);

            transform.rotation = Quaternion.Euler(newRotationX, 0f, 0f);

            float newY = Mathf.Lerp(transform.position.y, targetHeight, elapsedTime / turnDuration);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.rotation = Quaternion.Euler(targetRotationX, 0f, 0f);

        isTurning = false;
        boxCollider.enabled = false;

        elapsedTime = 0f;
        while (elapsedTime < disappearDuration)
        {
            foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
            {
                Debug.Log(renderer.material.color.a);
                renderer.material.color = new Vector4(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, renderer.material.color.a - (1f / disappearDuration) * Time.deltaTime * disappearRate);
                Debug.Log(renderer.material.color.a);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


}

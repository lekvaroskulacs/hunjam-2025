using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockroachMovement : MonoBehaviour
{
    [SerializeField] Transform start;
    [SerializeField] Transform end;

    [SerializeField] float speed = 2f;
    [SerializeField] float turnDuration = 0.2f;


    BoxCollider2D boxCollider;
    private Animator anim;
    private bool isTurning = false;
    private float facingDirection = 1f;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
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


}

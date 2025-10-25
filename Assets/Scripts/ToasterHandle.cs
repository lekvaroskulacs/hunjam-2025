using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterHandle : MonoBehaviour
{
    private Vector3 _startingPos;
    private Vector3 _endPos;
    private bool _isMoving = false;

    [Header("Settings")]
    public float moveDownOffset = 3f;    // How far down it moves
    public float moveDownSpeed = 2f;     // Speed when moving down
    public float moveUpSpeed = 4f;       // Speed when moving up
    public float stayDownTime = 3f;      // How long it stays down
    public float delayBeforeDown = 0.5f; // Delay before starting to move down

    private void Start()
    {
        _startingPos = transform.position;
        _endPos = _startingPos - new Vector3(0, moveDownOffset, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isMoving && collision.collider.CompareTag("Player"))
        {
            StartCoroutine(MoveHandle());
        }
    }

    private IEnumerator MoveHandle()
    {
        _isMoving = true;

        // Wait before moving down
        yield return new WaitForSeconds(delayBeforeDown);

        // Move down
        yield return StartCoroutine(MoveToPosition(_endPos, moveDownSpeed));

        // Stay down
        yield return new WaitForSeconds(stayDownTime);

        // Move back up
        yield return StartCoroutine(MoveToPosition(_startingPos, moveUpSpeed));

        // Print when it reaches the top
        Debug.Log("Toaster handle reached the top!");

        _isMoving = false;
    }

    private IEnumerator MoveToPosition(Vector3 targetPos, float speed)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
    }
}

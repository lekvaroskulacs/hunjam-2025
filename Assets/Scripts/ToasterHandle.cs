using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToasterHandle : MonoBehaviour
{
    [SerializeField] private GameObject _jumpPad; // The jump pad to activate briefly

    private Vector3 _startingPos;
    private Vector3 _endPos;
    private bool _isMoving = false;

    [Header("Settings")]
    public float moveDownOffset = 3f;    // How far down it moves
    public float moveDownSpeed = 2f;     // Speed when moving down
    public float moveUpSpeed = 4f;       // Speed when moving up
    public float stayDownTime = 3f;      // How long it stays down
    public float delayBeforeDown = 0.5f; // Delay before starting to move down
    public float jumpPadActiveTime = 0.2f; // How long the jump pad is active

    private void Start()
    {
        _startingPos = transform.position;
        _endPos = _startingPos - new Vector3(0, moveDownOffset, 0);

        // Ensure the jump pad starts off
        if (_jumpPad != null)
            _jumpPad.SetActive(false);
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

        Debug.Log("Toaster handle reached the top!");

        // Activate jump pad for a short time
        if (_jumpPad != null)
            StartCoroutine(ActivateJumpPad());

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

    private IEnumerator ActivateJumpPad()
    {
        _jumpPad.SetActive(true);
        yield return new WaitForSeconds(jumpPadActiveTime);
        _jumpPad.SetActive(false);
    }
}

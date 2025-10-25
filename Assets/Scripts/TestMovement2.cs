using UnityEngine;

public class TestMovement2 : MonoBehaviour
{
    public float speed =5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            direction += Vector3.up;
        if (Input.GetKey(KeyCode.S))
            direction += Vector3.down;
        if (Input.GetKey(KeyCode.A))
            direction += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            direction += Vector3.right;

        if (direction != Vector3.zero)
        {
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }
}

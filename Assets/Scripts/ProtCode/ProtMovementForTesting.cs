using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtMovementForTesting : MonoBehaviour
{
    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right arrows
        transform.position += new Vector3(moveInput, 0, 0) * 5f * Time.deltaTime;
    }
}

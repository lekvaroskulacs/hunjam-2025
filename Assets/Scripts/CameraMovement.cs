using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Tooltip("Assign the character Transform in the Inspector or leave empty to find GameObject named 'Character'.")]
    public Transform target;

    [Tooltip("If true, camera Y will smoothly follow the target.")]
    public bool smooth = true;
    [Tooltip("Smoothing speed when following.")]
    public float smoothSpeed = 5f;

    private void Start()
    {
        if (target == null)
        {
            var go = GameObject.Find("Character");
            if (go != null) target = go.transform;
        }
    }

    // Use LateUpdate to follow after character movement
    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 pos = transform.position;
        float targetY = target.position.y;

        if (smooth)
        {
            pos.y = Mathf.Lerp(pos.y, targetY, Time.deltaTime * smoothSpeed);
        }
        else
        {
            pos.y = targetY;
        }

        transform.position = pos;
    }
}

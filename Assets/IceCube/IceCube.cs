using System.Collections;
using System.Data.SqlTypes;
using UnityEngine;

public class IceCube : MonoBehaviour
{
    [SerializeField] private float squeezeDuration = 5f;
    private Vector3 scale;

    private Vector3 originalPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.position;
        InvokeRepeating("Respawn", 0f, squeezeDuration);
        scale = transform.localScale;
    }

    void Respawn()
    {
        transform.position = originalPosition;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        StartCoroutine(SqueezeAnim());
    }

    IEnumerator SqueezeAnim()
    {
        Vector3 originalScale = scale;
        Vector3 targetScale = new Vector3(originalScale.x, 0f, originalScale.z);
        float elapsedTime = 0f;

        while (elapsedTime < squeezeDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / squeezeDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localScale = targetScale;
    }
}

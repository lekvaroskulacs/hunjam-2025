using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Ketchup : MonoBehaviour
{
    [SerializeField] private float splatImpulseThreshold;
    [SerializeField] private float squeezeDuration;
    [SerializeField] private float squeezeTargetScale;

    private ParticleSystem particles;

    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.rigidbody)
            return;

        float totalImpulse = 0;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            totalImpulse += contact.normalImpulse;
        }

        if (totalImpulse >= splatImpulseThreshold)
        {
            if (collision.GetContact(0).point.y > transform.position.y) // Ensure collision is from above
                Splat();
        }
    }

    private void Splat()
    {
        Debug.Log("Ketchup splat!");
        
        particles.Play();
        StartCoroutine("SqueezeAnim");
    }

    IEnumerator SqueezeAnim()
    {
        Vector3 originalScale = Vector3.one;
        Vector3 targetScale = new Vector3(originalScale.x, squeezeTargetScale, originalScale.z);
        float elapsedTime = 0f;

        while (elapsedTime < squeezeDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / squeezeDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localScale = targetScale;

        elapsedTime = 0f;
        while (elapsedTime < squeezeDuration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / squeezeDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localScale = originalScale;
    }
}

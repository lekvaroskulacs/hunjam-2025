using UnityEngine;

public class KnifeSound : MonoBehaviour
{
    [SerializeField] public ParticleSystem hitParticles;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Knife collided with " + other.gameObject.name);

        // play assigned particle system at collision point
        if (hitParticles != null)
        {
            Debug.Log("Playing hit particles.");
            // position particle system at the point closest to the knife
            hitParticles.Play();
        }

        if (SoundManager.Instance != null)
        {
            Debug.Log("Playing 'knife_chop' SFX.");
            SoundManager.Instance.PlaySFX("knife_chop");
        }
    }
}

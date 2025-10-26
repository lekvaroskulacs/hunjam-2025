using UnityEngine;

public class DieRestart : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null || other.gameObject == null) return;
        Debug.Log("Triggered by: " + other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Died!");
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySFX("died");
                other.gameObject.GetComponent<Control>().Die();
            }
            else
            {
                Debug.LogWarning("SoundManager.Instance is null. Cannot play 'died' SFX.");
            }
        }
    }
}

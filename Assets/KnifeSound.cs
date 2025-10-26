using UnityEngine;

public class KnifeSound : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX("knife_chop");
        }
    }
}

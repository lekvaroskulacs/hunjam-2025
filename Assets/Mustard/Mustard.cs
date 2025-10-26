using UnityEngine;

public class Mustard : MonoBehaviour
{
    [SerializeField] private GameObject _brokenMustard;
    public void Explode()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        _brokenMustard.SetActive(true);
        Debug.Log("Mustard explosion effect triggered!");
        // Switch sprites to exlpoded version
    }
}

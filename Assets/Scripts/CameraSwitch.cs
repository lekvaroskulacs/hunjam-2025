using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _camera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _camera != null)
        {
            _camera.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _camera != null)
        {
            _camera.SetActive(false);
        }
    }
}

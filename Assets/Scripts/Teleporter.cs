using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public string interactScene = "KalmansScene";
    public string characterScene = "CharacterController";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;
        if (other.gameObject.name == "Character")
        {
            if (this.gameObject.name == "TestSceneChange1")
            {
                LoadTargetScene(interactScene);
            }
            else if (this.gameObject.name == "TestSceneChange2")
            {
                // Teleport the character to the GameObject named "Fridge_TP_point"
                var target = GameObject.Find("Fridge_TP_point");
                if (target == null)
                {
                    Debug.LogWarning("Fridge_TP_point not found in scene.");
                    return;
                }

                // If the character has a Rigidbody2D, move it via the Rigidbody to avoid physics issues
                var rb2d = other.attachedRigidbody;
                if (rb2d != null)
                {
                    rb2d.position = (Vector2)target.transform.position;
                    rb2d.velocity = Vector2.zero;
                }
                else
                {
                    other.transform.position = target.transform.position;
                }

                // Also move the main camera to the target position (preserve camera's Z)
                var cam = Camera.main;
                if (cam != null)
                {
                    Vector3 camPos = cam.transform.position;
                    camPos.x = target.transform.position.x;
                    camPos.y = target.transform.position.y;
                    cam.transform.position = camPos;
                }
            }
            else
            {
                LoadTargetScene(characterScene);
            }
        }
    }

    private void LoadTargetScene(string sceneToLoad)
    {
        if (string.IsNullOrWhiteSpace(sceneToLoad)) return;
        var name = sceneToLoad.EndsWith(".unity") ? sceneToLoad.Substring(0, sceneToLoad.Length -6) : sceneToLoad;
        SceneManager.LoadScene(name);
    }
}

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
                LoadTargetScene(interactScene);
            else
                LoadTargetScene(characterScene);
        }
    }

    private void LoadTargetScene(string sceneToLoad)
    {
        if (string.IsNullOrWhiteSpace(sceneToLoad)) return;
        var name = sceneToLoad.EndsWith(".unity") ? sceneToLoad.Substring(0, sceneToLoad.Length -6) : sceneToLoad;
        SceneManager.LoadScene(name);
    }
}

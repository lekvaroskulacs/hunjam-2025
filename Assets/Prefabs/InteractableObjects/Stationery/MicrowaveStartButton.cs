using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MicrowaveStartButton : InteractableBase
{
    [SerializeField] private MicrowaveHandle microwaveHandle;
    [SerializeField] private AudioSource microwaveStartSound;
    [SerializeField] private AudioSource microwaveDoneSound;
    [SerializeField] private AudioSource microwaveExplosionSound;
    [SerializeField] private Mustard mustard;


    [SerializeField] GameObject _brokenMicro;
    [SerializeField] GameObject _micro;

    // Optional: assign a Canvas that will be used for the white flash (should cover the screen).
    // We'll simply enable/disable the Canvas component for the flash.
    [SerializeField] private Canvas flashCanvas;
    [SerializeField] private float flashDuration =0.5f;

    private bool _isPorpuseFulfilled;

    public override void Interact()
    {
        if (_isPorpuseFulfilled) return;

        Debug.Log("Micro Started!!!");
        if (microwaveHandle.hasFork)
        {
            StartCoroutine(StartExplosionCountdown());
        }
        else
        {
            StartCoroutine(StartMicrowave());   
        }
    }

    public override void Select()
    {
        if (_isPorpuseFulfilled) return;
        base.Select();
    }

    public override void DeSelect()
    {
        if (_isPorpuseFulfilled) return;
        base.DeSelect();
    }
    IEnumerator StartMicrowave()
    {
        microwaveStartSound.Play();
        while (microwaveStartSound.isPlaying)
        {
            yield return null;
        }
        microwaveDoneSound.Play();
        Debug.Log("Microwave finished heating!");
    }

    IEnumerator StartExplosionCountdown()
    {
        microwaveStartSound.Play();
        while (microwaveStartSound.isPlaying)
        {
            yield return null;
        }
        microwaveExplosionSound.Play();
        // trigger explosion effect here
        mustard.Explode();

        _brokenMicro.SetActive(true);
        _micro.SetActive(false);

        // Start white flash effect using the assigned Canvas
        if (flashCanvas != null)
        {
            StartCoroutine(FlashWhite());
        }

        base.DeSelect();
        _isPorpuseFulfilled = true;
        Debug.Log("Boom! Microwave exploded due to fork inside!");
    }

    private IEnumerator FlashWhite()
    {
        if (flashCanvas == null) yield break;
        Debug.Log(flashCanvas.name);
        Debug.Log(flashCanvas.enabled);

        // Enable the Canvas component (do not modify other params)
        flashCanvas.enabled = true;

        yield return new WaitForSeconds(flashDuration);

        // Disable the Canvas component
        flashCanvas.enabled = false;

    }
}

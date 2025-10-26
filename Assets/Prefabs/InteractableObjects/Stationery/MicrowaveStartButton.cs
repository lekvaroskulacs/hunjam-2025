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

        //SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        //if (sprite != null)
        //{
        //    sprite.color = Color.red;
        //}
    }

    public override void DeSelect()
    {
        if (_isPorpuseFulfilled) return;
        base.DeSelect();

        //SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        //if (sprite != null)
        //{
        //    sprite.color = Color.black;
        //}
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

        _isPorpuseFulfilled = true;
        Debug.Log("Boom! Microwave exploded due to fork inside!");
    }
}

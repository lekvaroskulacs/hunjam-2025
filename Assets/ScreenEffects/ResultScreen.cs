using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    public GameObject ketchupCheck;
    public GameObject ketchupFail;
    public GameObject mustardCheck;
    public GameObject mustardFail;
    public GameObject bunCheck;
    public GameObject bunFail;

    public AudioSource eating;
    [SerializeField] private AudioSource bad;
    [SerializeField] private AudioSource meh;
    [SerializeField] private AudioSource good;
    [SerializeField] private AudioSource amazing;

    public void CheckResult(bool ketchup, bool mustard, bool bun)
    {
        int score = 0;
        if (ketchup)
        {
            ketchupCheck.SetActive(true);
            ++score;
        }
        else
        {
            ketchupFail.SetActive(true);
        }
        if (mustard)
        {
            mustardCheck.SetActive(true);
            ++score;
        }
        else
        {
            mustardFail.SetActive(true);
        }
        if (bun)
        {
            bunCheck.SetActive(true);
            ++score;
        }
        else
        {
            bunFail.SetActive(true);
        }

        StartCoroutine(PlaySound(score));

    }

    IEnumerator PlaySound(int score)
    {
        while (eating.isPlaying)
        {
            yield return null;
        }

        if (score == 0)
        {
            bad.Play();
        }
        else if (score == 1)
        {
            meh.Play();
        }
        else if (score == 2)
        {
            good.Play();
        }
        else if (score == 3)
        {
            amazing.Play();
        }
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

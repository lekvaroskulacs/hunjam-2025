using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NamedSFX
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Music")]
    public AudioClip musicClipA;
    public AudioClip musicClipB;
    [Tooltip("If true and both clips are assigned, musicClipA and musicClipB will play sequentially in a loop.")]
    public bool playSequence = true;
    [Range(0f, 1f)] public float musicVolume = 0.7f;

    [Header("SFX")]
    [Tooltip("Single AudioSource used for playing one-shot SFX. PlayOneShot supports overlapping clips.")]
    public float sfxVolume = 1f;
    public NamedSFX[] sfxCatalog;

    private AudioSource musicSource;
    private AudioSource sfxSource;
    private Dictionary<string, AudioClip> sfxLookup;

    private Coroutine musicCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Create and configure music AudioSource
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.playOnAwake = false;
        musicSource.loop = false; // we manage looping when using sequence
        musicSource.volume = musicVolume;

        // Create SFX AudioSource
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        sfxSource.loop = false;
        sfxSource.volume = sfxVolume;

        // Build lookup
        sfxLookup = new Dictionary<string, AudioClip>();
        if (sfxCatalog != null)
        {
            foreach (var entry in sfxCatalog)
            {
                if (entry.clip == null || string.IsNullOrEmpty(entry.name)) continue;
                if (!sfxLookup.ContainsKey(entry.name)) sfxLookup.Add(entry.name, entry.clip);
            }
        }
    }

    private void Start()
    {
        // If sequence enabled and both clips are assigned, start the looped sequence
        if (playSequence && musicClipA != null && musicClipB != null)
        {
            StartMusicSequence(new[] { musicClipA, musicClipB });
        }
        else if (musicClipA != null)
        {
            PlayMusic(musicClipA, musicVolume);
        }
    }

    /// <summary>
    /// Play background music clip (will play once unless you set loop on the AudioSource)
    /// </summary>
    public void PlayMusic(AudioClip clip, float volume = 0.7f)
    {
        if (clip == null) return;
        StopMusicSequenceIfRunning();
        musicSource.clip = clip;
        musicSource.volume = Mathf.Clamp01(volume);
        musicSource.loop = true; // single clip loops by default
        musicSource.Play();
    }

    public void StopMusic()
    {
        StopMusicSequenceIfRunning();
        if (musicSource.isPlaying) musicSource.Stop();
    }

    /// <summary>
    /// Start playing an ordered list of music clips one after another in a loop.
    /// </summary>
    public void StartMusicSequence(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;
        StopMusicSequenceIfRunning();
        musicCoroutine = StartCoroutine(MusicSequenceCoroutine(clips));
    }

    private void StopMusicSequenceIfRunning()
    {
        if (musicCoroutine != null)
        {
            StopCoroutine(musicCoroutine);
            musicCoroutine = null;
        }
    }

    private IEnumerator MusicSequenceCoroutine(AudioClip[] clips)
    {
        int idx = 0;
        while (true)
        {
            var clip = clips[idx];
            if (clip == null)
            {
                // skip nulls
                idx = (idx + 1) % clips.Length;
                yield return null;
                continue;
            }

            musicSource.loop = false;
            musicSource.clip = clip;
            musicSource.volume = Mathf.Clamp01(musicVolume);
            musicSource.Play();

            // wait for the clip to finish
            yield return new WaitForSeconds(clip.length);

            idx = (idx + 1) % clips.Length;
        }
    }

    /// <summary>
    /// Play a one-shot SFX audio clip
    /// </summary>
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, Mathf.Clamp01(volume));
    }

    /// <summary>
    /// Play a named SFX that exists in the sfxCatalog
    /// </summary>
    public void PlaySFX(string name, float volume = 1f)
    {
        if (string.IsNullOrEmpty(name)) return;
        if (sfxLookup != null && sfxLookup.TryGetValue(name, out var clip))
        {
            PlaySFX(clip, volume);
        }
        else
        {
            Debug.LogWarning($"SoundManager: SFX '{name}' not found in catalog.");
        }
    }

    /// <summary>
    /// Add or replace a named SFX at runtime
    /// </summary>
    public void RegisterSFX(string name, AudioClip clip)
    {
        if (string.IsNullOrEmpty(name) || clip == null) return;
        if (sfxLookup == null) sfxLookup = new Dictionary<string, AudioClip>();
        sfxLookup[name] = clip;
    }
}

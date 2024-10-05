using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource music;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Well, Nothing to Play!");
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (music.isPlaying)
        {
            StartCoroutine(FadeOutAndChangeMusic(clip, 1f));
        }

        else
        {
            music.clip = clip;
            music.Play();
        }
    }

    public IEnumerator FadeOutAndChangeMusic(AudioClip newClip, float fadeTime)
    {
        float startVolume = music.volume;

        while (music.volume > 0)
        {
            music.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        music.Stop();
        music.clip = newClip;
        music.volume = startVolume;
        music.Play();
    }
}

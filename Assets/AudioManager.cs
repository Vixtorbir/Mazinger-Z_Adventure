using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip jump;
    public AudioClip death;
    public AudioClip shoot;
    public AudioClip hit;
    public AudioClip get_damaged;
    public AudioClip enemydeath;
    public AudioClip respawn;
    public AudioClip tubo;


    private void Start()
    {
        musicSource.clip = background;
       
        musicSource.Play();
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}

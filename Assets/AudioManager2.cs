using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager2 : MonoBehaviour
{
    public AudioClip winMusic; // Arrastra tu clip de audio aqu� en el Inspector
    public AudioSource audioSource;

    private void Awake()
    {
        // Aseg�rate de que solo haya un AudioManager en la escena
        int numAudioManagers = FindObjectsOfType<AudioManager2>().Length;
        if (numAudioManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
            audioSource = GetComponent<AudioSource>();
            PlayWinMusic();
        }
    }

    public void PlayWinMusic()
    {
        audioSource.clip = winMusic; // Asigna el clip de m�sica
        audioSource.Play(); // Reproduce la m�sica
    }

    private void OnDestroy()
    {
        StopMusic(); // Aseg�rate de detener la m�sica cuando el AudioManager se destruya
    }

    public void StopMusic()
    {
        audioSource.Stop(); // Detiene la m�sica
    }
}

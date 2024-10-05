using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager2 : MonoBehaviour
{
    public AudioClip winMusic; // Arrastra tu clip de audio aquí en el Inspector
    public AudioSource audioSource;

    private void Awake()
    {
        // Asegúrate de que solo haya un AudioManager en la escena
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
        audioSource.clip = winMusic; // Asigna el clip de música
        audioSource.Play(); // Reproduce la música
    }

    private void OnDestroy()
    {
        StopMusic(); // Asegúrate de detener la música cuando el AudioManager se destruya
    }

    public void StopMusic()
    {
        audioSource.Stop(); // Detiene la música
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenController : MonoBehaviour
{
    // M�todo para cargar la escena del men� principal
    public void ChangeScreen()
    {
        // Detener la m�sica antes de cambiar de escena
        FindObjectOfType<AudioManager2>().StopMusic(); // Llama a StopMusic

        // Cambia a la escena de victoria
        SceneManager.LoadScene("Main-Menu-Example");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}

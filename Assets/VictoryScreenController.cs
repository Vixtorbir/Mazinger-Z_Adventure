using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenController : MonoBehaviour
{
    // Método para cargar la escena del menú principal
    public void ChangeScreen()
    {
        // Detener la música antes de cambiar de escena
        FindObjectOfType<AudioManager2>().StopMusic(); // Llama a StopMusic

        // Cambia a la escena de victoria
        SceneManager.LoadScene("Main-Menu-Example");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}

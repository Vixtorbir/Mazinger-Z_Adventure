using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText; // Referencia al UI Text que mostrará el puntaje
    private int score = 0;

    private void Awake()
    {
        // Asegurarse de que solo haya una instancia del ScoreManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreText();
    }

    // Método para agregar puntos
    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Método para resetear el puntaje a 0
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    // Actualiza el texto de la UI
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // Método para obtener el puntaje actual
    public int GetScore()
    {
        return score;
    }
}

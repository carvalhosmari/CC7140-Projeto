using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int score = 0;
    public static GameController instance;
    public TMP_Text scoreText;
    public GameObject gameOverPanel;

    [Header("Timer (Timed Mode only)")]
    public TimerController timerController;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    /// <summary>Updates the score HUD with the current score value.</summary>
    public void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    /// <summary>Stops the timer and activates the game over panel.</summary>
    public void GameOver()
    {
        if (timerController != null)
            timerController.StopTimer();

        gameOverPanel.SetActive(true);
    }

    /// <summary>Stops the timer when the player completes the level.</summary>
    public void LevelCompleted()
    {
        if (timerController != null)
            timerController.StopTimer();
    }

    public void RestartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

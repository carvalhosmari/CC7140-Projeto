using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("Score")]
    public int score = 0;
    public TMP_Text scoreText;

    [Header("Lives")]
    public TMP_Text livesText;

    [Header("Game Over")]
    public GameObject gameOverPanel;

    [Header("Timer (Timed Mode only)")]
    public TimerController timerController;

    void Start()
    {
        instance = this;

        // Ensure GameManager exists (fallback for editor play without menu scene).
        if (GameManager.Instance == null)
        {
            var go = new GameObject("GameManager");
            go.AddComponent<GameManager>();
        }

        UpdateLivesText();
    }

    /// <summary>Updates the score HUD with the current score value.</summary>
    public void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    /// <summary>
    /// Called when the player touches a hazard.
    /// Consumes one life in GameManager and either reloads the scene or triggers Game Over.
    /// </summary>
    public void TakeDamage()
    {
        bool hasLivesRemaining = GameManager.Instance.ConsumeLife();
        UpdateLivesText();

        if (!hasLivesRemaining)
        {
            GameOver();
        }
        else
        {
            ReloadScene();
        }
    }

    /// <summary>Reloads the current scene so all hazards and collectibles reset.</summary>
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>Stops the timer and activates the Game Over panel.</summary>
    public void GameOver()
    {
        if (timerController != null)
            timerController.StopTimer();

        GameManager.Instance.ResetLives();
        gameOverPanel.SetActive(true);
    }

    /// <summary>Stops the timer when the player completes the level.</summary>
    public void LevelCompleted()
    {
        if (timerController != null)
            timerController.StopTimer();
    }

    /// <summary>Resets lives and returns to the main menu.</summary>
    public void RestartGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.ResetLives();
        SceneManager.LoadScene("Menu");
    }

    private void UpdateLivesText()
    {
        if (livesText != null && GameManager.Instance != null)
            livesText.text = $"Vidas: {GameManager.Instance.CurrentLives}";
    }
}

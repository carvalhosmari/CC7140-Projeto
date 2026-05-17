using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }

    [Header("Score")]
    public int score = 0;
    public TMP_Text scoreText;

    [Header("Lives")]
    public TMP_Text livesText;

    [Header("Game Over")]
    public GameObject gameOverPanel;

    [Header("Timer (Timed Mode only)")]
    public TimerController timerController;

    // Guard flags — prevent multiple calls while a scene transition or game over is pending.
    private bool isGameOver = false;
    private bool isTransitioning = false;

    void Start()
    {
        instance = this;

        // Ensure GameManager exists (fallback for editor play without menu scene).
        if (GameManager.Instance == null)
        {
            var go = new GameObject("GameManager");
            go.AddComponent<GameManager>();
        }

        // Restore accumulated score from previous phases and snapshot the phase start.
        score = GameManager.Instance.AccumulatedScore;
        GameManager.Instance.BeginPhase();

        UpdateScoreText();
        UpdateLivesText();
    }

    /// <summary>Updates the score HUD with the current score value.</summary>
    public void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    /// <summary>
    /// Called when the player touches a hazard.
    /// Consumes one life in GameManager and either reloads the scene or triggers Game Over.
    /// </summary>
    public void TakeDamage()
    {
        // Ignore if we are already handling a transition or game over.
        if (isGameOver || isTransitioning) return;

        bool hasLivesRemaining = GameManager.Instance.ConsumeLife();
        UpdateLivesText();

        if (!hasLivesRemaining)
        {
            GameOver();
        }
        else
        {
            isTransitioning = true;
            // Roll score back to what it was at the start of this phase before reloading.
            GameManager.Instance.RollbackScore();
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
        if (isGameOver) return;
        isGameOver = true;

        if (timerController != null)
            timerController.StopTimer();

        GameManager.Instance.ResetLives();
        GameManager.Instance.ResetScore();
        gameOverPanel.SetActive(true);
    }

    /// <summary>Persists the current score and stops the timer when the player completes the phase.</summary>
    public void LevelCompleted()
    {
        if (isTransitioning) return;
        isTransitioning = true;

        GameManager.Instance.SaveScore(score);

        if (timerController != null)
            timerController.StopTimer();
    }

    /// <summary>Resets all persistent state and returns to the main menu.</summary>
    public void RestartGame()
    {
        isGameOver = false;
        isTransitioning = false;
        Time.timeScale = 1;
        GameManager.Instance.ResetLives();
        GameManager.Instance.ResetScore();
        SceneManager.LoadScene("Menu");
        Debug.Log("Restarting game...");
    }

    private void UpdateLivesText()
    {
        if (livesText != null && GameManager.Instance != null)
            livesText.text = $"{GameManager.Instance.CurrentLives}";
    }
}

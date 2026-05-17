using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the pause menu. Press Esc to toggle pause during gameplay.
/// Pausing freezes time and shows the pause panel; resuming restores it.
/// </summary>
public class PauseController : MonoBehaviour
{
    [Header("Pause Panel")]
    public GameObject pausePanel;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    /// <summary>Freezes time and shows the pause panel.</summary>
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    /// <summary>Restores time and hides the pause panel.</summary>
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    /// <summary>Returns to the main menu, resetting all persistent state.</summary>
    public void QuitToMainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetLives();
            GameManager.Instance.ResetScore();
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.StopMusic();

        SceneManager.LoadScene("Menu");
    }
}

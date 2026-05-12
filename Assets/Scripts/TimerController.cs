using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the countdown timer in Timed Mode.
/// Notifies GameController when time runs out.
/// </summary>
public class TimerController : MonoBehaviour
{
    [Tooltip("Text element that displays the remaining time.")]
    public TMP_Text timerText;

    private float timeRemaining;
    private bool isRunning = false;

    void Start()
    {
        bool isTimed = GameManager.Instance != null
            && GameManager.Instance.CurrentGameMode == GameManager.GameMode.Timed;

        if (isTimed)
        {
            timeRemaining = GameManager.LevelTimeLimit;
            isRunning = true;

            if (timerText != null)
                timerText.gameObject.SetActive(true);
        }
        else
        {
            if (timerText != null)
                timerText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isRunning) return;

        timeRemaining -= Time.deltaTime;

        UpdateTimerDisplay();

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            isRunning = false;
            GameController.instance.GameOver();
        }
    }

    /// <summary>Stops the timer (call this when the level is completed).</summary>
    public void StopTimer()
    {
        isRunning = false;
    }

    private void UpdateTimerDisplay()
    {
        if (timerText == null) return;

        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = $"Tempo: {seconds}s";

        // Visual warning when time is low
        timerText.color = seconds <= 10 ? Color.red : Color.white;
    }
}

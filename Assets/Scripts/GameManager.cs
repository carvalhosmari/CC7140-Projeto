using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Persistent singleton that carries the selected game mode across scenes.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameMode { Free, Timed }

    public GameMode CurrentGameMode { get; private set; } = GameMode.Free;

    public const float LevelTimeLimit = 45f;

    public const int MaxLives = 3;

    /// <summary>Lives remaining across the current run. Reset when starting a new game.</summary>
    public int CurrentLives { get; private set; } = MaxLives;

    /// <summary>Total score accumulated across all completed phases.</summary>
    public int AccumulatedScore { get; private set; } = 0;

    /// <summary>Score at the start of the current phase. Used to roll back on respawn.</summary>
    public int PhaseStartScore { get; private set; } = 0;

    /// <summary>Decrements one life. Returns true if any lives remain.</summary>
    public bool ConsumeLife()
    {
        if (CurrentLives > 0)
            CurrentLives--;
        return CurrentLives > 0;
    }

    /// <summary>Resets lives to the maximum (call when starting a new game).</summary>
    public void ResetLives()
    {
        CurrentLives = MaxLives;
    }

    /// <summary>
    /// Called when entering a new phase. Snapshots AccumulatedScore so a respawn
    /// can roll back to it without losing progress from previous phases.
    /// </summary>
    public void BeginPhase()
    {
        PhaseStartScore = AccumulatedScore;
    }

    /// <summary>Saves the total score when the player completes a phase.</summary>
    public void SaveScore(int totalScore)
    {
        AccumulatedScore = totalScore;
    }

    /// <summary>
    /// Rolls back AccumulatedScore to the value it had at the start of the current phase.
    /// Called before reloading the scene on respawn.
    /// </summary>
    public void RollbackScore()
    {
        AccumulatedScore = PhaseStartScore;
    }

    /// <summary>Resets the score completely (call when starting a new game).</summary>
    public void ResetScore()
    {
        AccumulatedScore = 0;
        PhaseStartScore = 0;
    }

    
    void Awake()
    {
        // Restore timescale in case it was paused by a previous session.
        Time.timeScale = 1;

        if (Instance != null && Instance != this)
        {
            // Destroy only this component, not the whole GameObject.
            // Other components on this object (e.g. MainMenuController) must remain alive.
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>Sets the game mode before loading the first level.</summary>
    public void SetGameMode(GameMode mode)
    {   
        // Destroy(gameObject);
        CurrentGameMode = mode;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}

using UnityEngine;

/// <summary>
/// Persistent singleton that carries the selected game mode across scenes.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameMode { Free, Timed }

    public GameMode CurrentGameMode { get; private set; } = GameMode.Free;

    public const float LevelTimeLimit = 45f;

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
}

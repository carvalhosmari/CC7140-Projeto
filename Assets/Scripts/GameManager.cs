using UnityEngine;

/// <summary>
/// Persistent singleton that carries the selected game mode across scenes.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameMode { Free, Timed }

    public GameMode CurrentGameMode { get; private set; } = GameMode.Free;

    public const float LevelTimeLimit = 30f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>Sets the game mode before loading the first level.</summary>
    public void SetGameMode(GameMode mode)
    {
        CurrentGameMode = mode;
    }
}

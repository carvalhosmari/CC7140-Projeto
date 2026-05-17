using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the main menu screen. Handles mode selection and scene transition.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    private const string FirstLevelSceneName = "Fase01";

    void Start()
    {
        EnsureAudioManager();
        AudioManager.Instance.PlayMainMenuMusic();
    }

    /// <summary>Called when the player clicks "Modo Livre".</summary>
    public void OnFreeModeSelected()
    {
        EnsureGameManager();
        GameManager.Instance.ResetLives();
        GameManager.Instance.ResetScore();
        GameManager.Instance.SetGameMode(GameManager.GameMode.Free);
        SceneManager.LoadScene(FirstLevelSceneName);
    }

    /// <summary>Called when the player clicks "Modo Tempo".</summary>
    public void OnTimedModeSelected()
    {
        EnsureGameManager();
        GameManager.Instance.ResetLives();
        GameManager.Instance.ResetScore();
        GameManager.Instance.SetGameMode(GameManager.GameMode.Timed);
        SceneManager.LoadScene(FirstLevelSceneName);
    }

    private void EnsureGameManager()
    {
        if (GameManager.Instance == null)
        {
            new GameObject("GameManager").AddComponent<GameManager>();
        }
    }

    private void EnsureAudioManager()
    {
        if (AudioManager.Instance == null)
        {
            new GameObject("AudioManager").AddComponent<AudioManager>();
        }
    }
}

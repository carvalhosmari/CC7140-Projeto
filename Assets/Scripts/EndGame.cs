using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    void Start()
    {
        // Ensure persistent state is clean when reaching the final screen.
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetLives();
            GameManager.Instance.ResetScore();
        }
    }

    /// <summary>Called by the Reset button on the final screen. Returns to the main menu.</summary>
    public void RestartGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
